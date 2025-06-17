using DevHobby.GPTizza.Contracts.Services;
using DevHobby.GPTizza.Model;
using Microsoft.AspNetCore.Components;

namespace DevHobby.GPTizza.Components.Pages;

public partial class SupportOverview
{
    public List<Ticket> Tickets { get; set; } = default!;
    private Pizza? _selectedPizza;
    
    [Inject]
    public ITicketDataService TicketDataService { get; set; }

    [Inject]
    public IHttpContextAccessor httpContextAccessor { get; set; }

    protected async override Task OnInitializedAsync()
    {
        var userName = httpContextAccessor.HttpContext.User.Identity.Name;

        Tickets = (await TicketDataService.GetTicketByCustomerId(userName)).ToList();
    }
}
