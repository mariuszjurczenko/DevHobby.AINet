using Azure.AI.Vision.ImageAnalysis;
using DevHobby.GPTizza.Contracts.Services;
using DevHobby.GPTizza.Model;
using DevHobby.GPTizza.Util;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using OpenAI;
using OpenAI.Chat;
using OpenAI.Images;
using System.ClientModel;

namespace DevHobby.GPTizza.Components.Admin.Pages;

public partial class PizzaAdd
{
    [SupplyParameterFromForm]
    public Pizza Pizza { get; set; }

    [Inject]
    public IPizzaDataService? PizzaDataService { get; set; }

    [Inject]
    public IOptions<ModelSettings> ModelSettings { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public OpenAIClient OpenAIClient { get; set; }

    [Inject]
    public ImageAnalysisClient ImageAnalysisClient { get; set; }

    private string Message = string.Empty;
    private bool Generating = false;
    private bool IsSaved = false;
    private string GeneratedImage = string.Empty;

    private ChatClient chatClient;
    private ImageClient imageClient;

    protected override void OnInitialized()
    {
        Pizza ??= new();

        chatClient = OpenAIClient.GetChatClient(ModelSettings.Value.TextModelName);
        imageClient = OpenAIClient.GetImageClient(ModelSettings.Value.ImageModelName);
    }

    private async Task OnSubmit()
    {
        await PizzaDataService.AddPizza(Pizza);
        IsSaved = true;
        Message = "Pizza została pomyślnie dodana";
    }

    private async Task GenerateShortDescription()
    {
        Pizza.ShortDescription = string.Empty;

        string prompt = "Utwórz opis składający się z 20 słów dla  " +Pizza.Name +  ". Jest on używany na stronie internetowej Dev - Hobby Pizza Shop, sklepu specjalizującego się w tworzeniu wspaniałych pizz.";

        AsyncCollectionResult<StreamingChatCompletionUpdate> completionUpdates = chatClient.CompleteChatStreamingAsync(prompt);

        await foreach (StreamingChatCompletionUpdate completionUpdate in completionUpdates)
        {
            foreach (ChatMessageContentPart contentPart in completionUpdate.ContentUpdate)
            {
                Pizza.ShortDescription += contentPart.Text;
                StateHasChanged();
            }
        }
    }

    private async Task GenerateLongDescription()
    {
        Pizza.LongDescription = string.Empty;

        string prompt = "Utwórz 100 - wyrazowy opis dla " +Pizza.Name + ". Jest on używany na stronie internetowej Dev - Hobby Pizza Shop, sklepu specjalizującego się w tworzeniu wspaniałych pizz.";

        AsyncCollectionResult<StreamingChatCompletionUpdate> completionUpdates = chatClient.CompleteChatStreamingAsync(prompt);

        await foreach (StreamingChatCompletionUpdate completionUpdate in completionUpdates)
        {
            foreach (ChatMessageContentPart contentPart in completionUpdate.ContentUpdate)
            {
                Pizza.LongDescription += contentPart.Text;
                StateHasChanged();
            }
        }
    }

    private async Task GenerateImage()
    {
        Generating = true;

        Pizza.ImageUrl = string.Empty;
        GeneratedImage = string.Empty;

        StateHasChanged();

        string prompt = $"Utwórz bardzo szczegółowy, fotorealistyczny i wizualnie atrakcyjny obraz pizzy {Pizza.Name} dla witryny specjalizującej się w sprzedaży pizzy. Pizza powinna być centralnym punktem, prezentowana na rustykalnym drewnianym stole z miękkim, rozmytym tłem, aby podkreślić główny temat. Upewnij się, że ciasto wygląda na złociste, lekko przypieczone i chrupiące, z subtelnym połyskiem świadczącym o świeżości. Ser powinien być ciągnący się i lekko przypieczony na brzegach, z apetycznym błyskiem. W przypadku pizz z owocami morza, mięsem lub warzywami, składniki powinny wyglądać na świeże, aromatyczne i obficie ułożone. W przypadku pizz klasycznych, jak Margherita, uchwyć kontrast między roztopionym serem, sosem pomidorowym i świeżą bazylią. Oświetlenie powinno być ciepłe i zachęcające, uwydatniające tekstury i kolory składników. Dodaj subtelne szczegóły, takie jak mały nóż do pizzy, widelec lub serwetka obok pizzy, aby dodać realizmu. Ogólna kompozycja powinna przywoływać poczucie domowego komfortu i pysznego, świeżo wypieczonego posiłku – idealna dla sekcji żywności witryny. Uwzględnij również wygenerowany długi opis pizzy: {Pizza.LongDescription}";

        ImageGenerationOptions imageGenerationOptions = new()
        {
            Quality = GeneratedImageQuality.High,
            Size = GeneratedImageSize.W1792xH1024,
            Style = GeneratedImageStyle.Vivid,
            ResponseFormat = GeneratedImageFormat.Bytes,
        };

        GeneratedImage generatedImage = await imageClient.GenerateImageAsync(prompt, imageGenerationOptions);
        BinaryData bytes = generatedImage.ImageBytes;
        var imageName = Guid.NewGuid().ToString();

        using FileStream stream = File.OpenWrite($"{Directory.GetCurrentDirectory()}/wwwroot/images/{imageName}.png");
        bytes.ToStream().CopyTo(stream);
        GeneratedImage = $"<img src='{NavigationManager.BaseUri}/images/{imageName}.png' width=500/>";
        Pizza.ImageUrl = $"{NavigationManager.BaseUri}/images/{imageName}.png";

        Generating = false;
        StateHasChanged();
    }

    private async Task GenerateAltText()
    {
        ImageAnalysisResult result = ImageAnalysisClient.Analyze(new Uri(Pizza.ImageUrl), VisualFeatures.Caption | VisualFeatures.Read);

        Pizza.AltText = result.Caption.Text;
    }
}
