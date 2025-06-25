using Azure;
using Azure.AI.TextAnalytics;

namespace DevHobby.AINet.UseAzureAI.Language;

public static class LanguageDetector
{
    public static void DetectLanguage()
    {
        string languageApiKey = Environment.GetEnvironmentVariable("LANGUAGE_KEY");
        string languageApiEndpoint = Environment.GetEnvironmentVariable("LANGUAGE_ENDPOINT");

        var keyCredentials = new AzureKeyCredential(languageApiKey);
        var endpointUri = new Uri(languageApiEndpoint);

        var textAnalyticsClient = new TextAnalyticsClient(endpointUri, keyCredentials);

        string descriptionText = "La nostra Pizza Quattro Formaggi Goddess è un vero capolavoro creato per i più profondi intenditori di formaggi, un omaggio alla ricchezza e alla diversità dei sapori. Su un impasto cotto alla perfezione, sottile e leggermente croccante, si adagia una lussuosa composizione di quattro classici italiani. La mozzarella vellutata e lattiginosa crea una base cremosa e filante, perfettamente bilanciata dalle note profonde, intense e leggermente piccanti del nobile gorgonzola. Il delicato e burroso provolone aggiunge una delicata dolcezza e morbidezza al tutto, creando un profilo aromatico deliziosamente complesso. Il tutto è coronato da parmigiano grattugiato fresco, che conferisce un accento salato e nocciolato e una consistenza cristallina.";

        DetectedLanguage identifiedLanguage = textAnalyticsClient.DetectLanguage(descriptionText);

        Console.WriteLine($"Dostarczony tekst: {descriptionText}");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"Wykryty język: {identifiedLanguage.Name}");
    }
}