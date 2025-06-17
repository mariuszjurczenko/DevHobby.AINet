using DevHobby.GPTizza.Contracts.Services;
using DevHobby.GPTizza.Model;
using Microsoft.AspNetCore.Components;

namespace DevHobby.GPTizza.Components.Admin.Pages;

public partial class PizzaDetail
{
    public Pizza Pizza { get; set; } = new Pizza();

    [Parameter]
    public int PizzaId { get; set; }

    [Inject]
    public IPizzaDataService? PizzaDataService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Pizza = await PizzaDataService.GetPizzaDetails(PizzaId);
    }
}
