using Azure;
using Azure.AI.TextAnalytics;
using DevHobby.GPTizza.Contracts.Repositories;
using DevHobby.GPTizza.Contracts.Services;
using DevHobby.GPTizza.Model;
using DevHobby.GPTizza.Util;
using Microsoft.Extensions.Options;
using OpenAI;
using OpenAI.Chat;
using System.Text;
using System.Text.Json;

namespace DevHobby.GPTizza.Services;

public class TicketDataService : ITicketDataService
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IPizzaRepository _pizzaRepository;
    private readonly OpenAIClient _openAIClient;
    private readonly TextAnalyticsClient _textAnalyticsClient;
    private readonly IOptions<ModelSettings> _modelSettings;
    private static JsonSerializerOptions SerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);

    public TicketDataService(ITicketRepository TicketRepository, 
        OpenAIClient openAIClient, 
        IOptions<ModelSettings> modelSettings, 
        IPizzaRepository pizzaRepository,
        TextAnalyticsClient textAnalyticsClient)
    {
        _ticketRepository = TicketRepository;
        _openAIClient = openAIClient;
        _modelSettings = modelSettings;
        _pizzaRepository = pizzaRepository;
        _textAnalyticsClient = textAnalyticsClient;
    }

    public async Task<Ticket> AddMessageToTicket(int ticketId, TicketMessage ticketMessage)
    {
        Pizza p = null;

        Ticket ticket = await _ticketRepository.GetTicketById(ticketId) ?? throw new Exception("Ticket nie został znaleziony");

        if (ticket.PizzaId != null)
        {
            p = await _pizzaRepository.GetPizzaById(ticket.PizzaId.Value);
        }

        ticket.TicketMessages.Add(ticketMessage);

        ticket.LastModifiedDate = DateTime.Now;

        await SummarizeTicket(ticket, p);

        return await _ticketRepository.UpdateTicket(ticket);
    }

    public async Task<Ticket> AddTicket(Ticket ticket)
    {
        Pizza p = null;

        if (ticket.PizzaId != null && ticket.PizzaId != 0)
        {
            p = await _pizzaRepository.GetPizzaById(ticket.PizzaId.Value);
        }

        ticket.CreatedDate = DateTime.Now;
        ticket.LastModifiedDate = DateTime.Now;

        await SummarizeTicket(ticket, p);
        await GetTicketMessageSentiment(ticket.TicketMessages.First());

        return await _ticketRepository.AddTicket(ticket);
    }

    public async Task DeleteTicket(int ticketId)
    {
        await _ticketRepository.DeleteTicket(ticketId);
    }

    public async Task<IEnumerable<Ticket>> GetAllTickets()
    {
        return await _ticketRepository.GetAllTickets();
    }

    public async Task<Ticket> GetTicketDetails(int ticketId)
    {
        return await _ticketRepository.GetTicketById(ticketId);
    }

    public async Task UpdateTicket(Ticket ticket)
    {
        await _ticketRepository.UpdateTicket(ticket);
    }

    public async Task<IEnumerable<Ticket>> GetTicketByCustomerId(string customerId)
    {
        return await _ticketRepository.GetTicketByCustomerId(customerId);
    }

    public async Task<IEnumerable<Ticket>> GetTicketsByStatus(TicketStatus ticketStatus)
    {
        return await _ticketRepository.GetTicketsByStatus(ticketStatus);
    }

    private async Task SummarizeTicket(Ticket ticket, Pizza pizza)
    {
        string[] satisfactionScores = ["Furious", "Unhappy", "Disappointed", "Indifferent", "Happy", "Delighted"];

        var prompt = $$"""
                Pomagasz w systemie obsługi zgłoszeń klienta.
                Twoim zadaniem jest dostarczanie podsumowań wiadomości zgłoszeń, aby umożliwić agentowi pomocy technicznej szybkie zrozumienie kontekstu zgłoszenia.

                Poniżej znajdują się szczegóły bieżącego zgłoszenia pomocy technicznej:

                Pizza: {{pizza?.Name ?? "Nie określone"}}

                Bieżący dziennik wiadomości wygląda następująco:

                {{CombineTicketMessages(ticket.TicketMessages)}}

                Wygeneruj następujące podsumowania:

                1. Zwięzłe podsumowanie, ograniczone do 30 słów, które zawiera jak najwięcej istotnych informacji. Unikaj powtarzania nazwy klienta lub produktu, ponieważ jest ona już znana. W szczególności wyróżnij wszelkie konkretne pytania lub szczegóły, o których wspomniano. Podsumuj kluczowe punkty, takie jak oczekujące pytania lub wymagane konkretne odpowiedzi, skupiając się na bieżącym statusie zgłoszenia i na tym, jaki rodzaj odpowiedzi od agenta pomocy technicznej byłby najbardziej korzystny.             

                2. Krótkie, 8-wyrazowe podsumowanie, które działa jako tytuł Ticketu. Celem tego jest podkreślenie, co jest wyjątkowego w tym Tickecie.

                3. Na koniec napisz 10-wyrazowe podsumowanie ostatniej wiadomości od KLIENTA, ignorując wszelkie odpowiedzi agenta. Opierając się wyłącznie na tym, oceń poziom zadowolenia klienta, używając jednego z następujących wskaźników zadowolenia:
                  {{string.Join(", ", satisfactionScores)}}.
                  Zwróć szczególną uwagę na ton głosu klienta, gdyż jego stan emocjonalny jest kluczowy dla tej oceny.

                Podsumowania będą przeznaczone wyłącznie do użytku wewnętrznego przez agentów obsługi klienta.

                Zwróć wynik w formacie JSON w następującym formacie: 
                {
                  "summary": "string",
                  "title": "string",
                  "customerSatisfaction": "string"
                }
                Upewnij się, że w wynikach nie ma żadnego znacznika markdown ani kodu HTML.
                """;

        var messageList = new List<ChatMessage>
            {
                new SystemChatMessage(prompt)
            };

        var chatService = _openAIClient.GetChatClient(_modelSettings.Value.TextModelName);
        var chatResponse = await chatService.CompleteChatAsync(messageList);

        var responseText = chatResponse.Value.Content.First().Text;

        var deserializedResponse = DeserializeResponse<ChatResponse>(responseText, SerializerOptions)!;

        var ticketSummary = deserializedResponse.Summary;
        var ticketTitle = deserializedResponse.Title;

        int? sentimentScore = Array.FindIndex(satisfactionScores, score => score == (deserializedResponse.CustomerSatisfaction ?? string.Empty));

        if (sentimentScore == -1)
        {
            sentimentScore = null;
        }

        ticket.CustomerSentimentScore = sentimentScore;
        ticket.Title = ticketTitle;
        ticket.Summary = ticketSummary;
    }

    private static TOutput? DeserializeResponse<TOutput>(string jsonContent, JsonSerializerOptions settings)
    {
        var jsonBytes = Encoding.UTF8.GetBytes(jsonContent);
        var jsonReader = new Utf8JsonReader(jsonBytes.AsSpan());
        return JsonSerializer.Deserialize<TOutput>(ref jsonReader, settings);
    }

    private string CombineTicketMessages(List<TicketMessage> messages)
    {
        var stringBuilder = new StringBuilder();

        foreach (var ticket in messages)
        {
            var role = ticket.IsSupportMessage ? "support" : "customer";
            stringBuilder.AppendLine($"<message role=\"{role}\">{ticket.Message}</message>");
        }

        return stringBuilder.ToString();
    }

    private async Task GetTicketMessageSentiment(TicketMessage ticketMessage)
    {
        Response<DocumentSentiment> response = await _textAnalyticsClient.AnalyzeSentimentAsync(
            ticketMessage.Message,
            options: new AnalyzeSentimentOptions());

        DocumentSentiment sentiment = response.Value;

        if ((sentiment.ConfidenceScores.Positive >= sentiment.ConfidenceScores.Negative)
         && (sentiment.ConfidenceScores.Positive >= sentiment.ConfidenceScores.Neutral))
        {
            ticketMessage.TicketMessageSentiment = TicketMessageSentiment.Positive;
        }
        else if (sentiment.ConfidenceScores.Negative >= sentiment.ConfidenceScores.Positive
              && sentiment.ConfidenceScores.Negative >= sentiment.ConfidenceScores.Neutral)
        {
            ticketMessage.TicketMessageSentiment = TicketMessageSentiment.Negative;
        }
        else
        {
            ticketMessage.TicketMessageSentiment = TicketMessageSentiment.Neutral;
        }
    }

    private class ChatResponse
    {
        public required string Summary { get; set; }
        public required string Title { get; set; }
        public string? CustomerSatisfaction { get; set; }
    }
}