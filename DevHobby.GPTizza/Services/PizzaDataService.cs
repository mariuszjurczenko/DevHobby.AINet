using DevHobby.GPTizza.Contracts.Repositories;
using DevHobby.GPTizza.Contracts.Services;
using DevHobby.GPTizza.Model;

namespace DevHobby.GPTizza.Services;

public class PizzaDataService : IPizzaDataService
{
    private readonly IPizzaRepository _pizzaRepository;

    public PizzaDataService(IPizzaRepository pizzaRepository)
    {
        _pizzaRepository = pizzaRepository;
    }

    public async Task<Pizza> AddPizza(Pizza Pizza)
    {
        return await _pizzaRepository.AddPizza(Pizza);
    }

    public async Task DeletePizza(int PizzaId)
    {
        await _pizzaRepository.DeletePizza(PizzaId);
    }

    public async Task<IEnumerable<Pizza>> GetAllPizzas()
    {
        return await _pizzaRepository.GetAllPizzas();
    }

    public async Task<Pizza> GetPizzaDetails(int PizzaId)
    {
        return await _pizzaRepository.GetPizzaById(PizzaId);
    }

    public async Task UpdatePizza(Pizza Pizza)
    {
        await _pizzaRepository.UpdatePizza(Pizza);
    }
}