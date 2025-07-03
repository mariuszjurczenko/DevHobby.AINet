using DevHobby.GPTizza.Contracts.Services;
using DevHobby.GPTizza.Util;
using Microsoft.AspNetCore.Components;
using Microsoft.CognitiveServices.Speech;
using Microsoft.Extensions.Options;
using OpenAI;
using OpenAI.Chat;
using System.ClientModel;

namespace DevHobby.GPTizza.Components.Admin.Pages;

public partial class PizzaCreator
{
    private ChatClient chatClient;

    private SpeechSynthesizer speechSynthesizer;

    [Inject]
    public OpenAIClient OpenAIClient { get; set; }

    [Inject]
    private SpeechSynthesizer SpeechSynthesizer { get; set; }

    [Inject]
    private IPizzaRecipeDataService PizzaRecipeDataService { get; set; }

    [Inject]
    public IOptions<ModelSettings> ModelSettings { get; set; }

    protected string Message = string.Empty;
    protected string Ingredients = string.Empty;
    protected bool IsSaved = false;

    override protected void OnInitialized()
    {
        chatClient = OpenAIClient.GetChatClient(ModelSettings.Value.TextModelName);
        speechSynthesizer = SpeechSynthesizer;
    }

    private async Task OnEnterIngredients()
    {
        Message = string.Empty;
        var json = await PizzaRecipeDataService.GetAllPizzaRecipesAsJson();

        var prompt = $$"""
                    Jesteś asystentem pomagającym kucharzom w Dev-Hobby Shop, sklepie, który sprzedaje najpyszniejsze pizze.
                    Otrzymasz listę przepisów z Dev-Hobby, wraz z listą składników dla każdego przepisu.

                    Twoim zadaniem jest sprawdzenie, na podstawie listy dostępnych składników, które pizze można zrobić z podanych składników. Wszystkie składniki w przepisie muszą być dostępne, jeśli brakuje jednego lub więcej, nie ma możliwości upieczenia pizzy. Ilości muszą być również wystarczające.

                    Możesz używać TYLKO przepisów dostępnych na liście. Nie możesz wymyślać innych przepisów. Jeśli znajdziesz jedną lub więcej pizz, które można zrobić, wypisz je. Jeśli nie możesz zrobić żadnej pizzy, powiedz "Nie mogę znaleźć niczego, co moglibyśmy upiec z tych składników".

                    Oto lista przepisów i składników:

                    Przepisy: {{json}}

                    Lista dostępnych składników jest następująca: {{Ingredients}}
                    """;

        AsyncCollectionResult<StreamingChatCompletionUpdate> completionUpdates = chatClient.CompleteChatStreamingAsync(prompt);

        await foreach (StreamingChatCompletionUpdate completionUpdate in completionUpdates)
        {
            foreach (ChatMessageContentPart contentPart in completionUpdate.ContentUpdate)
            {
                Message += contentPart.Text;
                StateHasChanged();
            }
        }

        await speechSynthesizer.SpeakTextAsync(Message);
    }
}
