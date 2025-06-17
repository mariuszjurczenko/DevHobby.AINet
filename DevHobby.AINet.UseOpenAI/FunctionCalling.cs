using OpenAI;
using OpenAI.Chat;
using System.Text.Json;

namespace UseOpenAIFromNET;

public static class FunctionCalling
{
    private static readonly ChatTool getClosestCinemaTool = ChatTool.CreateFunctionTool( 
        functionName: nameof(GetClosestCinema),
        functionDescription: "Znajdź najbliższe kino na podstawie lokalizacji użytkownika"
    );

    private static readonly ChatTool getWeatherAtCinemaTool = ChatTool.CreateFunctionTool(
        functionName: nameof(GetWeatherAtCinema),
        functionDescription: "Sprawdź pogodę w danym kinie",
        functionParameters: BinaryData.FromString("""
            {
                "type": "object",
                "properties": {
                    "location": {
                        "type": "string",
                        "description": "Miasto i Państwo"
                    },
                    "unit": {
                        "type": "string",
                        "enum": [ "celsius", "fahrenheit" ],
                        "description": "Jednostka temperatury, której należy użyć."
                    }
                },
                "required": [ "location" ]
            }
        """)
    );

    public static void SimpleFunctionCalling(string modelName)
    {
        bool requiresAction = true;
        OpenAIClient openAiClient = new(Environment.GetEnvironmentVariable("OPENAI_API_KEY"));
        ChatClient chatClient = openAiClient.GetChatClient(modelName);

        List<ChatMessage> chatHistory = [new UserChatMessage("Chcę pójść do najbliższego kina. Co powinienem założyć?")];

        ChatCompletionOptions options = new()
        {
            Tools = { getClosestCinemaTool, getWeatherAtCinemaTool },
        };

        while (requiresAction)
        {
            requiresAction = false;
            ChatCompletion response = chatClient.CompleteChat(chatHistory, options);

            switch (response.FinishReason)
            {
                case ChatFinishReason.ToolCalls:
                    {
                        chatHistory.Add(new AssistantChatMessage(response));

                        foreach (ChatToolCall toolCall in response.ToolCalls)
                        {
                            switch (toolCall.FunctionName)
                            {
                                case nameof(GetClosestCinema):
                                    {
                                        string toolResult = GetClosestCinema();
                                        chatHistory.Add(new ToolChatMessage(toolCall.Id, toolResult));
                                        break;
                                    }

                                case nameof(GetWeatherAtCinema):
                                    {
                                        using JsonDocument argumentsJson = JsonDocument.Parse(toolCall.FunctionArguments);
                                        bool hasLocation = argumentsJson.RootElement.TryGetProperty("location", out JsonElement location);
                                        bool hasUnit = argumentsJson.RootElement.TryGetProperty("unit", out JsonElement unit);

                                        if (!hasLocation)
                                        {
                                            throw new ArgumentNullException(nameof(location), "Argument lokalizacji jest wymagany.");
                                        }

                                        string toolResult = hasUnit
                                            ? GetWeatherAtCinema(location.GetString(), unit.GetString())
                                            : GetWeatherAtCinema(location.GetString());

                                        chatHistory.Add(new ToolChatMessage(toolCall.Id, toolResult));
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
                        chatHistory.Add(new AssistantChatMessage(response));
                        break;
                    }

                case ChatFinishReason.Length:
                    throw new NotImplementedException("Niekompletny wynik modelu z powodu przekroczenia parametru MaxTokens lub limitu tokenów.");

                case ChatFinishReason.ContentFilter:
                    throw new NotImplementedException("Pominięta treść z powodu flagi filtra treści.");

                case ChatFinishReason.FunctionCall:
                    throw new NotImplementedException("Wycofano na rzecz wywołań narzędzi.");

                default:
                    throw new NotImplementedException(response.FinishReason.ToString());
            }
        }

        foreach (ChatMessage message in chatHistory)
        {
            if (message.Content.Count > 0)
                Console.WriteLine(message.Content[0].Text);
        }
    }

    private static string GetClosestCinema()
    {
        // Ta aplikacja może teraz uzyskać aktualną lokalizację użytkownika i zwrócić najbliższe kino
        // Na razie zwrócimy zakodowaną na stałe lokalizację
        return "Katowice, Polska";
    }

    private static string GetWeatherAtCinema(string location, string unit = "celsius")
    {
        // Może to pochodzić z zewnętrznego interfejsu API pogodowego
        return $"25 {unit} i jest słonacznie.";
    }
}
