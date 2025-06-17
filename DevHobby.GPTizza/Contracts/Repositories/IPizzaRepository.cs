using DevHobby.GPTizza.Model;

namespace DevHobby.GPTizza.Contracts.Repositories;

public interface IPizzaRepository
{
    Task<IEnumerable<Pizza>> GetAllPizzas(); 
    Task<Pizza> GetPizzaById(int PizzaId);
    Task<Pizza> AddPizza(Pizza Pizza);
    Task<Pizza> UpdatePizza(Pizza Pizza);
    Task DeletePizza(int PizzaId);
}
