using DevHobby.AINet.UseSemanticKernel.Plugins;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace DevHobby.AINet.UseSemanticKernel;

public class UsingPlugins
{
    public async Task GetDaysUntilNewYearsEve(string modelName)
    {
        Kernel kernel = Kernel.CreateBuilder().AddOpenAIChatCompletion(
            modelId: modelName, apiKey: Environment.GetEnvironmentVariable("OPENAI_API_KEY")).Build();

        kernel.ImportPluginFromType<DateTimePlugin>();

        OpenAIPromptExecutionSettings settings = new() { ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions };

        Console.WriteLine(await kernel.InvokePromptAsync("Ile dni pozostało do Sylwestra w tym roku? ", new(settings)));
    }

    public async Task ChatWithMultiplePlugins(string modelName)
    {
        Kernel kernel = Kernel.CreateBuilder().AddOpenAIChatCompletion(
            modelId: modelName, apiKey: Environment.GetEnvironmentVariable("OPENAI_API_KEY")).Build();

        kernel.ImportPluginFromType<DateTimePlugin>();
        kernel.ImportPluginFromType<WeatherPlugin>();

        OpenAIPromptExecutionSettings settings = new() { ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions };

        var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();
        ChatHistory chatHistory = new();
        
        string response = string.Empty;
        while (response != "koniec")
        {
            Console.WriteLine("Wpisz swoją wiadomość:");
            response = Console.ReadLine();
            chatHistory.AddUserMessage(response);

            var assistantMessage = await chatCompletionService.GetChatMessageContentAsync(chatHistory, settings, kernel);
            Console.WriteLine(assistantMessage);
            chatHistory.Add(assistantMessage);
        }
    }
}
