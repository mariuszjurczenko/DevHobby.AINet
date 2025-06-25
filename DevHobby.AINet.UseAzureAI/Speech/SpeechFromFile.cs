using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace DevHobby.AINet.UseAzureAI.Speech;

public static class SpeechFromFile
{
    public static async Task ExecuteSpeechRecognitionAsync()
    {
        string apiKey = Environment.GetEnvironmentVariable("SPEECH_KEY");
        string apiRegion = Environment.GetEnvironmentVariable("SPEECH_REGION");

        RecognitionResult? recognitionResult = null;
        var speechConfig = SpeechConfig.FromSubscription(apiKey, apiRegion);
        speechConfig.SpeechRecognitionLanguage = "pl-PL";

        using (var audioSource = AudioConfig.FromWavFileInput("Audio/devhobby.wav"))
        {
            using (var speechRecognizer = new SpeechRecognizer(speechConfig, audioSource))
            {
                recognitionResult = await speechRecognizer.RecognizeOnceAsync().ConfigureAwait(false);
            }
        }

        if (recognitionResult != null)
        {
            Console.WriteLine($"Rozpoznano mowę: {recognitionResult.Text}");
        }
        else
        {
            Console.WriteLine("Nie można było rozpoznać mowy na podstawie nagrania audio.");
        }
    }
}
