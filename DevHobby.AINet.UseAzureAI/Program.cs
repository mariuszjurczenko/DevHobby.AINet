using DevHobby.AINet.UseAzureAI.Language;
using DevHobby.AINet.UseAzureAI.Speech;
using DevHobby.AINet.UseAzureAI.Translations;
using DevHobby.AINet.UseAzureAI.Vision;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder();
builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Language
// LanguageDetector
// LanguageDetector.DetectLanguage();

// SummarizeText
// await SummarizeText.SummarizeContentAsync();

// SentimentAnalysis
// SentimentAnalysis.AnalyzeCustomerSentiment();

// EntityRecognition
// EntityRecognizer.AnalyzeEntities();

// Translation
// await TextTranslator.PerformTextTranslationAsync();

// Vision
// Image caption generation
// ImageCaptionGenerator.CreateImageDescription();

// Tag generation
// TagGenerator.ExtractImageTags();

// Object recognition
// ObjectRecognition.DetectObjectsInImage();

// Speech
// Speech from file
await SpeechFromFile.ExecuteSpeechRecognitionAsync();


Console.ReadLine();