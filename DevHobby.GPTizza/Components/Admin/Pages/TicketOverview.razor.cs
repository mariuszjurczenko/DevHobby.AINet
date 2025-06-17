using DevHobby.GPTizza.Contracts.Services;
using DevHobby.GPTizza.Model;
using Microsoft.AspNetCore.Components;

namespace DevHobby.GPTizza.Components.Admin.Pages;

public partial class TicketOverview
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

        Tickets = (await TicketDataService.GetTicketsByStatus(TicketStatus.Open)).ToList();
    }
}
