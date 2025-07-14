using DevHobby.AINet.UseSemanticKernel;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder();
builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

IConfiguration config = builder.Build();

var modelName = config["modelName"];
var imageModel = config["imageModelName"];

// 1. AIChatExamples
// RunBasicPromptLoop
// await new AIChatExamples().RunBasicPromptLoop(modelName);

// RunChatWithHistory
// await new AIChatExamples().RunChatWithHistory(modelName);

// RunLiveResponseChat
// await new AIChatExamples().RunLiveResponseChat(modelName);

// ExperimentWithAISettings
// await new AIChatExamples().ExperimentWithAISettings(modelName);

//2. Image generation
await new ImageGeneration().GenerateBasicImage(imageModel);