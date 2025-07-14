using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace DevHobby.AINet.UseSemanticKernel;

public class AIChatExamples
{
    public async Task RunBasicPromptLoop(string modelName)
    {
        Kernel kernel = Kernel.CreateBuilder().AddOpenAIChatCompletion(
            modelId: modelName, apiKey: Environment.GetEnvironmentVariable("OPENAI_API_KEY")).Build();

        string userInput = string.Empty;
        while (userInput != "koniec")
        {
            Console.WriteLine("Zapytaj AI o cokolwiek:");
            userInput = Console.ReadLine();
            var result = await kernel.InvokePromptAsync(userInput);
            Console.WriteLine("\nOdpowiedź AI:");
            Console.WriteLine(result);
        }
    }

    public async Task RunChatWithHistory(string modelName)
    {
        Kernel kernel = Kernel.CreateBuilder().AddOpenAIChatCompletion(
            modelId: modelName, apiKey: Environment.GetEnvironmentVariable("OPENAI_API_KEY")).Build();

        var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();
        ChatHistory chatHistory = new();

        string userInput = string.Empty;
        while (userInput != "koniec")
        {
            Console.WriteLine("Zapytaj AI o cokolwiek:");
            userInput = Console.ReadLine();
            chatHistory.AddUserMessage(userInput);

            var assistantMessage = await chatCompletionService.GetChatMessageContentAsync(chatHistory);
            Console.WriteLine(assistantMessage);
            chatHistory.Add(assistantMessage);
        }
    }

    public async Task RunLiveResponseChat(string modelName)
    {
        Kernel kernel = Kernel.CreateBuilder().AddOpenAIChatCompletion(
            modelId: modelName, apiKey: Environment.GetEnvironmentVariable("OPENAI_API_KEY")).Build();

        var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

        ChatHistory chatHistory = new("Jesteś twórcą pizz i powinieneś wymyślać wspaniałe i pyszne przepisy na pizze.");

        chatHistory.AddAssistantMessage("Witamy na czacie twórcy pizz. Jak mogę ci dzisiaj pomóc??");
        var message = chatHistory.Last();
        Console.WriteLine($"{message.Role}: {message.Content}");

        chatHistory.AddUserMessage("Chcę zrobić pizze carbonare.");
        message = chatHistory.Last();
        Console.WriteLine($"{message.Role}: {message.Content}");

        await foreach (StreamingChatMessageContent chatUpdate in chatCompletionService.GetStreamingChatMessageContentsAsync(chatHistory))
        {
            Console.Write(chatUpdate.Content);
        }
    }

    public async Task ExperimentWithAISettings(string modelName)
    {
        Kernel kernel = Kernel.CreateBuilder().AddOpenAIChatCompletion(modelId: modelName, apiKey: Environment.GetEnvironmentVariable("OPENAI_API_KEY")).Build();

        KernelArguments arguments = new(new OpenAIPromptExecutionSettings { MaxTokens = 500, Temperature = 0 });
        Console.WriteLine("Temperatura 0:");
        Console.WriteLine(await kernel.InvokePromptAsync(
            "Opowiedz mi historię o Pizzeri GPTizza z Katowic, który słynie z pysznych pizz", arguments));

        arguments = new(new OpenAIPromptExecutionSettings { MaxTokens = 500, Temperature = 1 });
        Console.WriteLine("Temperatura 1:");
        Console.WriteLine(await kernel.InvokePromptAsync(
            "Opowiedz mi historię o Pizzeri GPTizza z Katowic, który słynie z pysznych pizz", arguments));
    }
}
