using Azure;
using Azure.AI.TextAnalytics;

namespace DevHobby.AINet.UseAzureAI.Language;

public static class SentimentAnalysis
{
    public static void AnalyzeCustomerSentiment()
    {
        string languageApiKey = Environment.GetEnvironmentVariable("LANGUAGE_KEY");
        string languageApiEndpoint = Environment.GetEnvironmentVariable("LANGUAGE_ENDPOINT");

        var keyCredentials = new AzureKeyCredential(languageApiKey);
        var endpointUri = new Uri(languageApiEndpoint);

        var textAnalyticsClient = new TextAnalyticsClient(endpointUri, keyCredentials);

        string feedback = "Absolutne mistrzostwo! Ta Pizza Quattro Formaggi Bogini przerosła wszystkie moje oczekiwania. Sery były po prostu niebiański – idealnie zbalansowany, tworzący kremową, bogatą kompozycję, w której czuć było każdy pojedynczy smak: od delikatnej mozzarelli, przez wyrazistą gorgonzolę, aż po wspaniały, orzechowy finisz parmezanu. Ciasto było perfekcyjne: cienkie, z fantastycznie chrupiącymi, wypieczonymi brzegami, które idealnie utrzymywały całe to serowe bogactwo. Pizza przyjechała gorąca, pachnąca i wyglądała niezwykle apetycznie. Warta każdej złotówki. To bez wątpienia najlepsza pizza czterech serów, jaką jadłem/jadłam. Na pewno zamówię ponownie!";

        DocumentSentiment documentSentiment = textAnalyticsClient.AnalyzeSentiment(feedback, options: new AnalyzeSentimentOptions());

        Console.WriteLine($"Ogólne nastawienie: {documentSentiment.Sentiment}\n");
        Console.WriteLine($"\tWynik pozytywny: {documentSentiment.ConfidenceScores.Positive:0.00}");
        Console.WriteLine($"\tWynik negatywny: {documentSentiment.ConfidenceScores.Negative:0.00}");
        Console.WriteLine($"\tWynik neutralny: {documentSentiment.ConfidenceScores.Neutral:0.00}\n");

        foreach (var sentence in documentSentiment.Sentences)
        {
            Console.WriteLine($"\tZdanie: \"{sentence.Text}\"");
            Console.WriteLine($"\tSentyment: {sentence.Sentiment}");
            Console.WriteLine($"\tWynik pozytywny: {sentence.ConfidenceScores.Positive:0.00}");
            Console.WriteLine($"\tWynik negatywny: {sentence.ConfidenceScores.Negative:0.00}");
            Console.WriteLine($"\tWynik neutralny: {sentence.ConfidenceScores.Neutral:0.00}\n");

        }
        Console.WriteLine();
    }
}
