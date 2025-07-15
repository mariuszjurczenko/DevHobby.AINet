using DevHobby.GPTizza.Contracts.Services;
using DevHobby.GPTizza.Model;
using DevHobby.GPTizza.Util;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.TextToImage;

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
    public Kernel Kernel { get; set; }

    private string Message = string.Empty;
    private bool Generating = false;
    private bool IsSaved = false;
    private string GeneratedImage = string.Empty;

    private IChatCompletionService chatCompletionService;
    private ITextToImageService textToImageService;

    protected override void OnInitialized()
    {
        Pizza ??= new();

        chatCompletionService = Kernel.GetRequiredService<IChatCompletionService>();
        textToImageService = Kernel.GetRequiredService<ITextToImageService>();
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

        ChatHistory chatHistory = new(prompt);

        await foreach (StreamingChatMessageContent chatUpdate in chatCompletionService.GetStreamingChatMessageContentsAsync(chatHistory))
        {
            Pizza.ShortDescription += chatUpdate.Content;
            StateHasChanged();
        }
    }

    private async Task GenerateLongDescription()
    {
        Pizza.LongDescription = string.Empty;

        string prompt = "Utwórz 100 - wyrazowy opis dla " +Pizza.Name + ". Jest on używany na stronie internetowej Dev - Hobby Pizza Shop, sklepu specjalizującego się w tworzeniu wspaniałych pizz.";

        ChatHistory chatHistory = new(prompt);

        await foreach (StreamingChatMessageContent chatUpdate in chatCompletionService.GetStreamingChatMessageContentsAsync(chatHistory))
        {
            Pizza.LongDescription += chatUpdate.Content;
            StateHasChanged();
        }
    }

    private async Task GenerateImage()
    {
        Generating = true;

        Pizza.ImageUrl = string.Empty;
        GeneratedImage = string.Empty;

        StateHasChanged();

        string prompt = $"Utwórz bardzo szczegółowy, fotorealistyczny i wizualnie atrakcyjny obraz pizzy {Pizza.Name} dla witryny specjalizującej się w sprzedaży pizzy. Pizza powinna być centralnym punktem, prezentowana na rustykalnym drewnianym stole z miękkim, rozmytym tłem, aby podkreślić główny temat. Upewnij się, że ciasto wygląda na złociste, lekko przypieczone i chrupiące, z subtelnym połyskiem świadczącym o świeżości. Ser powinien być ciągnący się i lekko przypieczony na brzegach, z apetycznym błyskiem. W przypadku pizz z owocami morza, mięsem lub warzywami, składniki powinny wyglądać na świeże, aromatyczne i obficie ułożone. W przypadku pizz klasycznych, jak Margherita, uchwyć kontrast między roztopionym serem, sosem pomidorowym i świeżą bazylią. Oświetlenie powinno być ciepłe i zachęcające, uwydatniające tekstury i kolory składników. Dodaj subtelne szczegóły, takie jak mały nóż do pizzy, widelec lub serwetka obok pizzy, aby dodać realizmu. Ogólna kompozycja powinna przywoływać poczucie domowego komfortu i pysznego, świeżo wypieczonego posiłku – idealna dla sekcji żywności witryny. Uwzględnij również wygenerowany długi opis pizzy: {Pizza.LongDescription}";

        var imageUrl = await textToImageService.GenerateImageAsync(prompt, 1792, 1024);

        string fileName = $"{Guid.NewGuid()}.png";
        await DownloadImageToFolder(imageUrl, fileName);

        GeneratedImage = $"<img src='{NavigationManager.BaseUri}/images/{fileName}' width=500/>";
        Pizza.ImageUrl = $"{NavigationManager.BaseUri}/images/{fileName}";

        Generating = false;
        StateHasChanged();
    }

    private async Task DownloadImageToFolder(string imageUrl, string fileName)
    {
        using (var client = new HttpClient())
        {
            var response = await client.GetAsync(imageUrl);
            response.EnsureSuccessStatusCode();

            using (var stream = await response.Content.ReadAsStreamAsync())
            using (var fileStream = new FileStream($"{Directory.GetCurrentDirectory()}/wwwroot/images/{fileName}", FileMode.Create))
            {
                await stream.CopyToAsync(fileStream);
            }
        }
    }
}
