using DevHobby.GPTizza.Contracts.Repositories;
using DevHobby.GPTizza.Data;
using DevHobby.GPTizza.Model;
using Microsoft.EntityFrameworkCore;

namespace DevHobby.GPTizza.Repositories;

public class PizzaRepository : IPizzaRepository, IDisposable
{
    private readonly ApplicationDbContext _applicationDbContext;

    public PizzaRepository(IDbContextFactory<ApplicationDbContext> DbFactory)
    {
        _applicationDbContext = DbFactory.CreateDbContext();
    }

    public async Task<IEnumerable<Pizza>> GetAllPizzas()
    {
        return await _applicationDbContext.Pizzas.ToListAsync();
    }

    public async Task<Pizza> GetPizzaById(int pizzaId)
    {
        return await _applicationDbContext.Pizzas.FirstOrDefaultAsync(c => c.Id == pizzaId);
    }

    public async Task<Pizza> AddPizza(Pizza pizza)
    {
        var addedEntity = await _applicationDbContext.Pizzas.AddAsync(pizza);
        await _applicationDbContext.SaveChangesAsync();
        return addedEntity.Entity;
    }

    public async Task DeletePizza(int pizzaId)
    {
        var foundPizza = await _applicationDbContext.Pizzas.FirstOrDefaultAsync(e => e.Id == pizzaId);
        if (foundPizza == null) return;

        _applicationDbContext.Pizzas.Remove(foundPizza);
        await _applicationDbContext.SaveChangesAsync();
    }

    public async Task<Pizza> UpdatePizza(Pizza Pizza)
    {
        var foundPizza = await _applicationDbContext.Pizzas.FirstOrDefaultAsync(e => e.Id == Pizza.Id);

        if (foundPizza != null)
        {
            foundPizza.Name = Pizza.Name;
            foundPizza.IsPizzaOfTheWeek = Pizza.IsPizzaOfTheWeek;
            foundPizza.ShortDescription = Pizza.ShortDescription;
            foundPizza.LongDescription = Pizza.LongDescription;
            foundPizza.Price = Pizza.Price;
            foundPizza.ImageUrl = Pizza.ImageUrl;

            await _applicationDbContext.SaveChangesAsync();

            return foundPizza;
        }

        return null;
    }

    public void Dispose()
    {
        _applicationDbContext.Dispose();
    }
}
