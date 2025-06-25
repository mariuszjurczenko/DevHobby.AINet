using Azure;
using Azure.AI.TextAnalytics;

namespace DevHobby.AINet.UseAzureAI.Language;

public static class EntityRecognizer
{
    public static void AnalyzeEntities()
    {
        string languageApiKey = Environment.GetEnvironmentVariable("LANGUAGE_KEY");
        string languageApiEndpoint = Environment.GetEnvironmentVariable("LANGUAGE_ENDPOINT");

        var keyCredentials = new AzureKeyCredential(languageApiKey);
        var endpointUri = new Uri(languageApiEndpoint);

        var textAnalyticsClient = new TextAnalyticsClient(endpointUri, keyCredentials);

        string description = "Nasza Pizza Quattro Formaggi Bogini to prawdziwe arcydzieło stworzone dla najgłębszych koneserów sera, hołd dla bogactwa i różnorodności smaków. Na idealnie wypieczonym, cienkim i lekko chrupiącym cieście spoczywa luksusowa kompozycja czterech włoskich klasyków. Aksamitna i mleczna mozzarella tworzy kremową, ciągnącą się podstawę, która jest idealnie równoważona przez głębokie, intensywne i lekko pikantne nuty szlachetnej gorgonzoli. Delikatne, maślane provolone dodaje całości subtelnej słodyczy i gładkości, tworząc zachwycająco złożony profil smakowy. Dzieło wieńczy świeżo starty, twardy parmezan, który wnosi słony, orzechowy akcent i krystaliczną teksturę.";

        CategorizedEntityCollection extractedEntities = textAnalyticsClient.RecognizeEntities(description);

        if (extractedEntities.Count > 0)
        {
            Console.WriteLine("\nWyodrębnione jednostki (Entities) :");
            foreach (var entity in extractedEntities)
            {
                Console.WriteLine($"Tekst: {entity.Text}");
                Console.WriteLine($"Długość jednostki: {entity.Length}");
                Console.WriteLine($"Kategoria: {entity.Category}");

                if (!string.IsNullOrWhiteSpace(entity.SubCategory))
                    Console.WriteLine($"Podkategoria: {entity.SubCategory}");

                Console.WriteLine($"Pewność: {entity.ConfidenceScore}");
                Console.WriteLine();
            }
        }
    }
}
