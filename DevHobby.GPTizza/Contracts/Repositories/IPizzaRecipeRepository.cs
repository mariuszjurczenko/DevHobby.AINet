using DevHobby.GPTizza.Model;

namespace DevHobby.GPTizza.Contracts.Repositories;

public interface IPizzaRecipeRepository
{
    Task<IEnumerable<PizzaRecipe>> GetAllPizzaRecipes();
}
