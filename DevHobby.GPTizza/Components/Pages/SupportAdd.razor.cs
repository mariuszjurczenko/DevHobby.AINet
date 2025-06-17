using DevHobby.GPTizza.Contracts.Services;
using DevHobby.GPTizza.Model;
using DevHobby.GPTizza.Util;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;

namespace DevHobby.GPTizza.Components.Pages;

public partial class SupportAdd
{
    [SupplyParameterFromForm]
    public Ticket Ticket { get; set; }

    [SupplyParameterFromForm]
    public string InitialMessage { get; set; } = string.Empty;

    [Inject]
    public ITicketDataService? TicketDataService { get; set; }

    [Inject]
    public IPizzaDataService PizzaDataService { get; set; }

    [Inject]
    public IOptions<ModelSettings> ModelSettings { get; set; }

    [Inject]
    public IHttpContextAccessor httpContextAccessor { get; set; }

    protected string Message = string.Empty;
    protected bool IsSaved = false;

    public List<Pizza> Pizzas { get; set; } = [];


    protected async override Task OnInitializedAsync()
    {
        Ticket ??= new();

        Pizzas = (await PizzaDataService.GetAllPizzas()).ToList();
        Pizzas.Insert(0, new Pizza { Id = 0, Name = "Niepowiązane" });
    }

    private async Task OnSubmit()
    {
        Ticket.TicketMessages =
        [
            new TicketMessage 
            { 
                Message = InitialMessage, 
                CreatedDate = DateTime.Now, 
                IsSupportMessage = false 
            },
        ];

        Ticket.CustomerId = httpContextAccessor.HttpContext.User.Identity.Name;
        await TicketDataService.AddTicket(Ticket);

        IsSaved = true;
        Message = "Ticket został pomyślnie wysłany";
    }
}
