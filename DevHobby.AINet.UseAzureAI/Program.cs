using DevHobby.AINet.UseAzureAI.Language;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder();
builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Language
// LanguageDetector
// LanguageDetector.DetectLanguage();

// SummarizeText
// await SummarizeText.SummarizeContentAsync();

// SentimentAnalysis
SentimentAnalysis.AnalyzeCustomerSentiment();


Console.ReadLine();