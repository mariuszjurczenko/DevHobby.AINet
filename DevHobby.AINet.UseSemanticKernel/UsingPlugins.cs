using DevHobby.AINet.UseSemanticKernel.Plugins;
using Microsoft.SemanticKernel;
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
}
