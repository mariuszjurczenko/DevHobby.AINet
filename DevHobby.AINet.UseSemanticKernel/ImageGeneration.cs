using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.TextToImage;

namespace DevHobby.AINet.UseSemanticKernel;

public class ImageGeneration
{
    public async Task GenerateBasicImage(string modelName)
    {
        Kernel kernel = Kernel.CreateBuilder().AddOpenAITextToImage(modelId: modelName, apiKey: Environment.GetEnvironmentVariable("OPENAI_API_KEY")).Build();

        ITextToImageService imageService = kernel.GetRequiredService<ITextToImageService>();

        string prompt =
           """
            Koncepcja salonu łącząca skandynawską prostotę z japońskim minimalizmem, zapewniająca
            spokojną i przytulną atmosferę. To przestrzeń, która zachęca do relaksu i uważności, z naturalnym światłem
            i świeżym powietrzem. Neutralne odcienie, w tym kolory takie jak biel, beż, szarość i czerń, tworzą
            poczucie harmonii. Eleganckie drewniane meble o czystych liniach i subtelnych krzywiznach, które dodają ciepła i
            elegancji. Rośliny i kwiaty w ceramicznych doniczkach dodają przestrzeni koloru i życia. Mogą służyć jako punkty centralne,
            tworząc połączenie z naturą. Miękkie tekstylia i poduszki z organicznych tkanin, dodające komfortu
            i miękkości przestrzeni. Mogą służyć jako akcenty, dodając kontrastu i faktury.          
            """;

        var image = await imageService.GenerateImageAsync(prompt, 1024, 1024);

        Console.WriteLine("Image URL: " + image);
    }
}
