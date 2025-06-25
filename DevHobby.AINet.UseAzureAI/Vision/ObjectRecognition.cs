using Azure;
using Azure.AI.Vision.ImageAnalysis;

namespace DevHobby.AINet.UseAzureAI.Vision;

public static class ObjectRecognition
{
    public static void DetectObjectsInImage()
    {
        string visionApiKey = Environment.GetEnvironmentVariable("VISION_KEY");
        string visionApiEndpoint = Environment.GetEnvironmentVariable("VISION_ENDPOINT");

        var credentials = new AzureKeyCredential(visionApiKey);
        var serviceUri = new Uri(visionApiEndpoint);

        var imageAnalysisClient = new ImageAnalysisClient(serviceUri, credentials);

        using var imageStream = new FileStream("Images/obrazek.png", FileMode.Open);

        ImageAnalysisResult analysisResult = imageAnalysisClient.Analyze(BinaryData.FromStream(imageStream), VisualFeatures.Objects);

        foreach (var detectedObject in analysisResult.Objects.Values)
        {
            Console.WriteLine($"Obiekt: '{detectedObject.Tags.First().Name}', Pole ograniczające: {detectedObject.BoundingBox}");
        }
    }
}
