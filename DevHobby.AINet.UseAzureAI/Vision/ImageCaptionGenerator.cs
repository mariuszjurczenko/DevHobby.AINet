using Azure;
using Azure.AI.Vision.ImageAnalysis;

namespace DevHobby.AINet.UseAzureAI.Vision;

public static class ImageCaptionGenerator
{
    public static void CreateImageDescription()
    {
        string visionApiKey = Environment.GetEnvironmentVariable("VISION_KEY");
        string visionApiEndpoint = Environment.GetEnvironmentVariable("VISION_ENDPOINT");

        var credentials = new AzureKeyCredential(visionApiKey);
        var serviceUri = new Uri(visionApiEndpoint);

        var imageAnalysisClient = new ImageAnalysisClient(serviceUri, credentials);

        using var imageStream = new FileStream("Images/obrazek.png", FileMode.Open);

        ImageAnalysisResult analysisResult = imageAnalysisClient.Analyze(BinaryData.FromStream(imageStream), VisualFeatures.Caption);

        Console.WriteLine($"Opis obrazu: '{analysisResult.Caption.Text}' z pewnością {analysisResult.Caption.Confidence:F4}");
    }
}
