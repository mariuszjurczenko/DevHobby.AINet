using DevHobby.AINet.UseOpenAI;
using Microsoft.Extensions.Configuration;
using UseOpenAIFromNET;

var builder = new ConfigurationBuilder();
builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

IConfiguration config = builder.Build();
var modelName = config["modelName"];
var imageModelName = config["imageModelName"];

//1. Connect with OpenAI using Http
//await ConnectWithHttp.Run();

//2. Chat completions
//ChatCompletions.SimpleChat(modelName);
//ChatCompletions.SimpleChatUsingOpenAIClient(modelName);
//await ChatCompletions.SimpleChatAsync(modelName);
//await ChatCompletions.SimpleChatStreamingAsync(modelName);
//await ChatCompletions.SimpleChatUsingOpenAIClientWithMessages(modelName);
//ChatCompletions.CreateCourseDescription(modelName);
//ChatCompletions.CreateCourseName(modelName);
//ChatCompletions.CreateFancyAndPoshCourseDescription(modelName);
//ChatCompletions.CreateCourseChapters(modelName);
//ChatCompletions.CreateCourseDescriptionWithOptions(modelName);
//await ChatCompletions.OpenEndedChatStreamingAsync(modelName);
//FunctionCalling.SimpleFunctionCalling(modelName);
//ImageGenerations.GenerateImageVariation(imageModelName);
Vision.DescribeImage(modelName);

Console.WriteLine();
Console.WriteLine("Gotowe");
