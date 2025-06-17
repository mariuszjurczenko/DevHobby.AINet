using DevHobby.GPTizza.Model;

namespace DevHobby.GPTizza.Contracts.Services;

public interface IPizzaDataService
{
    Task<IEnumerable<Pizza>> GetAllPizzas();
    Task<Pizza> GetPizzaDetails(int PizzaId);
    Task<Pizza> AddPizza(Pizza Pizza);
    Task UpdatePizza(Pizza Pizza);
    Task DeletePizza(int PizzaId);
}
