using DevHobby.GPTizza.Contracts.Services;
using DevHobby.GPTizza.Model;
using Microsoft.AspNetCore.Components;

namespace DevHobby.GPTizza.Components.Pages;

public partial class SupportDetail
{
    public Ticket Ticket { get; set; }

    [Parameter]
    public int TicketId { get; set; }

    [Inject]
    public ITicketDataService? TicketDataService { get; set; }

    [SupplyParameterFromForm]
    public TicketMessage TicketMessage { get; set; } = new TicketMessage();


    protected string Message = string.Empty;
    protected bool IsSaved = false;

    protected override async Task OnInitializedAsync()
    {
        Ticket = await TicketDataService.GetTicketDetails(TicketId);
    }

    private async Task OnSubmit()
    {
        TicketMessage.IsSupportMessage = false;

        await TicketDataService.AddMessageToTicket(TicketId, TicketMessage);

        IsSaved = true;
        Message = "Wiadomość została pomyślnie dodana";
    }
}
