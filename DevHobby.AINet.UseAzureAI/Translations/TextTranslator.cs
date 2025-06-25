using Azure;
using Azure.AI.Translation.Text;

namespace DevHobby.AINet.UseAzureAI.Translations;

public static class TextTranslator
{
    public static async Task PerformTextTranslationAsync()
    {
        string translationApiKey = Environment.GetEnvironmentVariable("TRANSLATION_KEY");
        string translationApiRegion = Environment.GetEnvironmentVariable("TRANSLATION_REGION");

        var credentials = new AzureKeyCredential(translationApiKey);

        var translationClient = new TextTranslationClient(credentials, translationApiRegion);

        string targetLanguage = "en";
        string sourceText = "Nasza Pizza Quattro Formaggi Bogini to prawdziwe arcydzieło stworzone dla najgłębszych koneserów sera, hołd dla bogactwa i różnorodności smaków. Na idealnie wypieczonym, cienkim i lekko chrupiącym cieście spoczywa luksusowa kompozycja czterech włoskich klasyków. Aksamitna i mleczna mozzarella tworzy kremową, ciągnącą się podstawę, która jest idealnie równoważona przez głębokie, intensywne i lekko pikantne nuty szlachetnej gorgonzoli. Delikatne, maślane provolone dodaje całości subtelnej słodyczy i gładkości, tworząc zachwycająco złożony profil smakowy. Dzieło wieńczy świeżo starty, twardy parmezan, który wnosi słony, orzechowy akcent i krystaliczną teksturę..";

        var translationResponse = await translationClient.TranslateAsync(targetLanguage, sourceText);
        var translatedItems = translationResponse.Value;
        var translationResult = translatedItems.FirstOrDefault();

        Console.WriteLine($"Wykryto oryginalny język: {translationResult?.DetectedLanguage?.Language} z poziomem pewności {translationResult?.DetectedLanguage?.Confidence}.");
        Console.WriteLine($"Przetłumaczony tekst (angielski): '{translationResult?.Translations?.FirstOrDefault()?.Text}'.");
    }
}
