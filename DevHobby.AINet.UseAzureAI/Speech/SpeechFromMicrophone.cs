using Microsoft.CognitiveServices.Speech;

namespace DevHobby.AINet.UseAzureAI.Speech;

public static class SpeechFromMicrophone
{
    public static async Task CaptureSpeechAsync()
    {
        string apiKey = Environment.GetEnvironmentVariable("SPEECH_KEY");
        string apiRegion = Environment.GetEnvironmentVariable("SPEECH_REGION");

        var speechConfig = SpeechConfig.FromSubscription(apiKey, apiRegion);
        speechConfig.SpeechRecognitionLanguage = "pl-PL";

        using var speechRecognizer = new SpeechRecognizer(speechConfig);

        Console.WriteLine("Proszę mówić do mikrofonu...");

        var recognitionResult = await speechRecognizer.RecognizeOnceAsync().ConfigureAwait(false);

        switch (recognitionResult.Reason)
        {
            case ResultReason.RecognizedSpeech:
                Console.WriteLine($"Rozpoznana mowa: \"{recognitionResult.Text}\"");
                break;

            case ResultReason.NoMatch:
                Console.WriteLine("Przepraszam, nie rozpoznałem żadnej mowy.");
                break;

            case ResultReason.Canceled:
                var cancelDetails = CancellationDetails.FromResult(recognitionResult);
                Console.WriteLine($"Operacja anulowana: {cancelDetails.Reason}");
                break;

            default:
                Console.WriteLine("Nieoczekiwany wynik podczas rozpoznawania mowy.");
                break;
        }
    }
}
