using DevHobby.GPTizza.Contracts.Services;
using DevHobby.GPTizza.Model;
using Microsoft.AspNetCore.Components;

namespace DevHobby.GPTizza.Components.Admin.Pages;

public partial class PizzaOverview
{
    public List<Pizza> Pizzas { get; set; } = default!;

    [Inject]
    public IPizzaDataService PizzaDataService { get; set; }


    protected async override Task OnInitializedAsync()
    {
        Pizzas = (await PizzaDataService.GetAllPizzas()).ToList();
    }
}
