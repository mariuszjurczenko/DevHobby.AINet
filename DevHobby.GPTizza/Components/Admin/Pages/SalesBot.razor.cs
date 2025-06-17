using DevHobby.GPTizza.Contracts.Services;
using DevHobby.GPTizza.Util;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using OpenAI.Chat;
using OpenAI;
using System.Text.Json;

namespace DevHobby.GPTizza.Components.Admin.Pages;

public partial class SalesBot
{
    private ChatClient chatClient;

    [Inject]
    public OpenAIClient OpenAIClient { get; set; }

    [Inject]
    private IOrderDataService OrderDataService { get; set; }

    [Inject]
    public IOptions<ModelSettings> ModelSettings { get; set; }

    protected string Message = string.Empty;
    protected string Question = string.Empty;
    protected bool IsSaved = false;

    override protected void OnInitialized()
    {
        chatClient = OpenAIClient.GetChatClient(ModelSettings.Value.TextModelName);
    }

    private async Task OnEnterQuestion()
    {
        bool requiresAction = true;

        List<ChatMessage> messages = [
            new SystemChatMessage("Jesteś botem sprzedażowym, który pomaga pracownikom Dev-Hobby Pizza Shop zrozumieć sprzedaż i dostarcza informacji na temat sprzedaży pizzy na przestrzeni czasu."),
            new UserChatMessage(Question)];

        ChatCompletionOptions options = new()
        {
            Tools = { getNumberOfPizzasSoldInGivenMonthAndYearTool }, 
        };

        while (requiresAction)
        {
            requiresAction = false;
            ChatCompletion response = await chatClient.CompleteChatAsync(messages, options);

            switch (response.FinishReason)
            {
                case ChatFinishReason.ToolCalls:
                    {
                        messages.Add(new AssistantChatMessage(response));

                        foreach (ChatToolCall toolCall in response.ToolCalls)
                        {
                            switch (toolCall.FunctionName)
                            {
                                case nameof(GetNumberOfPizzasSoldInGivenMonthAndYear):
                                    {
                                        using JsonDocument argumentsJson = JsonDocument.Parse(toolCall.FunctionArguments);
                                        bool hasMonth = argumentsJson.RootElement.TryGetProperty("month", out JsonElement month);
                                        bool hasYear = argumentsJson.RootElement.TryGetProperty("year", out JsonElement year);

                                        if (!hasMonth)
                                        {
                                            throw new ArgumentNullException(nameof(month), "Argument miesiąca jest wymagany");
                                        }

                                        if (!hasYear)
                                        {
                                            throw new ArgumentNullException(nameof(year), "Argument rok jest wymagany.");
                                        }

                                        int toolResult = await GetNumberOfPizzasSoldInGivenMonthAndYear(month.GetString(), year.GetString());

                                        messages.Add(new ToolChatMessage(toolCall.Id, toolResult.ToString()));
                                        break;
                                    }

                                default:
                                    {
                                        throw new NotImplementedException();
                                    }
                            }
                        }

                        requiresAction = true;
                        break;
                    }

                case ChatFinishReason.Stop:
                    {
                        messages.Add(new AssistantChatMessage(response));
                        break;
                    }

                case ChatFinishReason.Length:
                    throw new NotImplementedException("Niekompletne dane wyjściowe modelu ze względu na przekroczenie parametru MaxTokens lub limitu tokenów.");

                case ChatFinishReason.ContentFilter:
                    throw new NotImplementedException("Pominięto treść z powodu flagi filtra treści.");

                default:
                    throw new NotImplementedException(response.FinishReason.ToString());
            }
        }

        foreach (ChatMessage message in messages)
        {
            if (message.Content.Count > 0)
            {
                if (message is AssistantChatMessage)
                    Message += message.Content[0].Text;
            }
        }
    }

    private static readonly ChatTool getNumberOfPizzasSoldInGivenMonthAndYearTool = ChatTool.CreateFunctionTool(
        functionName: nameof(GetNumberOfPizzasSoldInGivenMonthAndYear),
        functionDescription: "Uzyskaj sprzedaż pizzy w bezwzględnej liczbie sprzedanych sztuk w Dev-Hobby w danym miesiącu (jako liczba od 1 do 12) i rok jako liczbę dla wszystkich pizz razem wziętych. Jeśli chcesz porównać miesiące, musisz wywołać tę funkcję wiele razy z różnymi wartościami dla miesiąca i roku, aby uzyskać sprzedaż dla danego okresu. Jeśli zwrócono -1, poinformuj użytkownika, że ​​nie udało się znaleźć sprzedaży dla danego okresu.",
        functionParameters: BinaryData.FromString("""
            {
                "type": "object",
                "properties": {
                    "month": {
                        "type": "string",
                        "description": "Miesiąc, w którym chcemy uzyskać sprzedaż"
                    },
                    "year": {
                        "type": "string",
                        "description": "Rok, w którym chcemy uzyskać sprzedaż."
                    }
                },
                "required": [ "month", "year" ]
            }
            """)
    );

    private async Task<int> GetNumberOfPizzasSoldInGivenMonthAndYear(string month, string year)
    {
        var orders = (await OrderDataService.GetAllOrdersForMonthAndYear(int.Parse(month), int.Parse(year))).ToList();

        if (orders.Count == 0)
        {
            return -1;
        }

        int sum = 0;
        foreach (var orderLine in orders.SelectMany(order => order.OrderLines))
        {
            sum += orderLine.Amount;
        }

        return sum;
    }
}
