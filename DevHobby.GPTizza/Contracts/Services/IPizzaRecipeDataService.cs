using DevHobby.GPTizza.Model;

namespace DevHobby.GPTizza.Contracts.Services;

public interface IPizzaRecipeDataService
{
    Task<IEnumerable<PizzaRecipe>> GetAllPizzaRecipes();
    Task<string> GetAllPizzaRecipesAsJson();
}
