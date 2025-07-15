using DevHobby.GPTizza.Contracts.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.ComponentModel;

namespace DevHobby.GPTizza.Components.Admin.Pages;

public partial class SalesBot
{
    [Inject]
    public Kernel Kernel { get; set; }

    protected string Message = string.Empty;
    protected string Question = string.Empty;
    protected bool IsSaved = false;

    private IChatCompletionService chatCompletionService;
    private OpenAIPromptExecutionSettings settings;

    override protected void OnInitialized()
    {
        chatCompletionService = Kernel.GetRequiredService<IChatCompletionService>();
        Kernel.ImportPluginFromType<SalesInformationPlugin>();
        settings = new() { ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions };
    }

    private async Task OnEnterQuestion()
    {
        ChatHistory chatHistory = new();

        chatHistory.AddSystemMessage("Jesteś botem sprzedażowym, który pomaga pracownikom Dev-Hobby Pizza Shop zrozumieć sprzedaż i dostarcza informacji na temat sprzedaży pizzy na przestrzeni czasu.");
        chatHistory.AddUserMessage(Question);

        var assistantMessage = await chatCompletionService.GetChatMessageContentAsync(chatHistory, settings, Kernel);
        Message = assistantMessage.Content;
        StateHasChanged();
    }
}

public class SalesInformationPlugin
{
    private IOrderDataService _orderDataService;

    public SalesInformationPlugin(IOrderDataService orderDataService)
    {
        _orderDataService = orderDataService;
    }

    [KernelFunction]
    [Description("Uzyskaj sprzedaż pizzy w bezwzględnej liczbie sprzedanych sztuk w Dev-Hobby w danym miesiącu (jako liczba od 1 do 12) i rok jako liczbę dla wszystkich pizz razem wziętych. Jeśli chcesz porównać miesiące, musisz wywołać tę funkcję wiele razy z różnymi wartościami dla miesiąca i roku, aby uzyskać sprzedaż dla danego okresu. Jeśli zwrócono -1, poinformuj użytkownika, że ​​nie udało się znaleźć sprzedaży dla danego okresu.")]
    public async Task<int> GetNumberOfPiesSoldInGivenMonthAndYear(string month, string year)
    {
        var orders = (await _orderDataService.GetAllOrdersForMonthAndYear(int.Parse(month), int.Parse(year))).ToList();

        if (orders.Count == 0)
        {
            return -1;
        }

        int sum = 0;
        foreach (var orderLine in orders.SelectMany(order => order.OrderLines))
        {
            sum += orderLine.Amount;
        }

        return sum;
    }
}