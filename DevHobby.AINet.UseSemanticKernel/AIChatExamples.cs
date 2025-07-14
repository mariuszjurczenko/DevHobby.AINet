using Microsoft.SemanticKernel;

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
}
