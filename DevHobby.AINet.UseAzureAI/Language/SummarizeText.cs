using Azure;
using Azure.AI.TextAnalytics;

namespace DevHobby.AINet.UseAzureAI.Language;

public static class SummarizeText
{
    public static async Task SummarizeContentAsync()
    {
        string languageApiKey = Environment.GetEnvironmentVariable("LANGUAGE_KEY");
        string languageApiEndpoint = Environment.GetEnvironmentVariable("LANGUAGE_ENDPOINT");

        var keyCredentials = new AzureKeyCredential(languageApiKey);
        var endpointUri = new Uri(languageApiEndpoint);

        var textAnalyticsClient = new TextAnalyticsClient(endpointUri, keyCredentials);

        var storyContent = "Zlokalizowana w sercu Katowic, w Polsce, GPTizza stała się ukochanym kulinarnym punktem na mapie miasta od swojego założenia w 2021 roku. Historia tej wyjątkowej pizzerii zaczyna się od Jana Cybulskiego, pasjonata informatyki i gastronomii, który marzył o połączeniu tradycyjnych, włoskich receptur z nowoczesną precyzją w tętniącym życiem śląskim mieście. Dorastając w Katowicach, Jan nauczył się szacunku do rzemiosła od swojego dziadka, mistrza piekarskiego, którego chleb był znany w całej okolicy. Zainspirowany włoską pasją do prostoty i śląską dbałością o jakość, Jan postanowił stworzyć miejsce, które zaoferuje mieszkańcom miasta pizzę doskonałą – produkt kulinarnego dziedzictwa i innowacyjnego myślenia.\r\n\r\nZ laptopem pełnym algorytmów na idealne ciasto i sercem pełnym pasji, Jan otworzył swój nowoczesny, a zarazem przytulny lokal przy tętniącej życiem ulicy Mariackiej. Od pierwszego dnia pizzeria stała się hitem. Zarówno mieszkańcy, jak i turyści byli przyciągani energią lokalu i obłędnym aromatem świeżo wypiekanej pizzy, który unosił się w powietrzu. To, co wyróżniało GPTizzę, to zaangażowanie Jana w wykorzystywanie wyłącznie najlepszych, starannie wyselekcjonowanych składników z Polski i Włoch. Wszystko przygotowywał od podstaw, od idealnie elastycznego, długo fermentującego ciasta po bogate, autentyczne sosy i dodatki. W jego menu znalazła się pyszna mieszanka klasycznych i nowatorskich pizz, od tradycyjnej Margherity DOC po innowacyjne kompozycje, takie jak \"Śląska Carbonara\" z wędzonym boczkiem i śmietaną czy pizza z oscypkiem i żurawiną.\r\n\r\nPrzez lata GPTizza rozwinęła się z nowatorskiego pomysłu w cenioną instytucję na kulinarnej scenie Katowic. Sam Jan stał się lokalnie znany ze swojego nowatorskiego podejścia i niezachwianego przywiązania do jakości. Dzisiaj lokal wciąż jest prowadzony z tą samą energią, a zespół nieustannie eksperymentuje z nowymi „algorytmami smaku”, kontynuując dziedzictwo założyciela. Pizzeria niezmiennie zachwyca klientów swoją niezwykłą pizzą i przyjazną atmosferą, sprawiając, że każda wizyta w GPTizzy jest prawdziwie niezapomnianym przeżyciem w sercu Katowic.";

        var documents = new List<string> { storyContent };

        ExtractiveSummarizeOperation summaryOperation = textAnalyticsClient.ExtractiveSummarize(WaitUntil.Completed, documents);

        await foreach (var summaryPage in summaryOperation.Value)
        {
            foreach (var summary in summaryPage)
            {
                Console.WriteLine($"Podsumowanie wyodrębnione ze  {summary.Sentences.Count} zdań:");
                Console.WriteLine();

                foreach (var sentence in summary.Sentences)
                {
                    Console.WriteLine($" - {sentence.Text}");
                }
            }
        }
    }
}
