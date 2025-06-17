using OpenAI.Images;

namespace UseOpenAIFromNET;

public static class ImageGenerations
{
    public static void GenerateImage(string modelName)
    {
        ImageClient client = new(modelName, Environment.GetEnvironmentVariable("OPENAI_API_KEY"));

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

        Console.WriteLine("Generowanie obrazu...");

        ImageGenerationOptions imageGenerationOptions = new()
        {
            Quality = GeneratedImageQuality.High,
            Size = GeneratedImageSize.W1792xH1024,
            Style = GeneratedImageStyle.Vivid,
            ResponseFormat = GeneratedImageFormat.Bytes,
        };

        GeneratedImage generatedImage = client.GenerateImage(prompt, imageGenerationOptions);
        BinaryData bytes = generatedImage.ImageBytes;

        using FileStream stream = File.OpenWrite($"{Guid.NewGuid()}.png");
        bytes.ToStream().CopyTo(stream);
    }

    public static void GenerateImageVariation(string modelName)
    {
        ImageClient client = new(modelName, Environment.GetEnvironmentVariable("OPENAI_API_KEY"));

        string image = "images/images_dog_and_cat.png";

        ImageVariationOptions imageVariationOptions = new()
        {
            Size = GeneratedImageSize.W512xH512,
            ResponseFormat = GeneratedImageFormat.Bytes,
        };

        GeneratedImage variation = client.GenerateImageVariation(image, imageVariationOptions);
        BinaryData bytes = variation.ImageBytes;

        using FileStream stream = File.OpenWrite($"{Guid.NewGuid()}.png");
        bytes.ToStream().CopyTo(stream);
    }
}
