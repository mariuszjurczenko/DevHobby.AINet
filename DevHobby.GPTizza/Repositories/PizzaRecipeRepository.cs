using DevHobby.GPTizza.Contracts.Repositories;
using DevHobby.GPTizza.Data;
using DevHobby.GPTizza.Model;
using Microsoft.EntityFrameworkCore;

namespace DevHobby.GPTizza.Repositories;

public class PizzaRecipeRepository : IPizzaRecipeRepository 
{
    private readonly ApplicationDbContext _applicationDbContext;

    public PizzaRecipeRepository(IDbContextFactory<ApplicationDbContext> DbFactory)
    {
        _applicationDbContext = DbFactory.CreateDbContext();
    }

    public async Task<IEnumerable<PizzaRecipe>> GetAllPizzaRecipes()
    {
        return await _applicationDbContext.PizzaRecipes.ToListAsync();
    }
}
