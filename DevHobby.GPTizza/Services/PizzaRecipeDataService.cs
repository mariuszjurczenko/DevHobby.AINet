using System.Text.Json.Serialization;
using System.Text.Json;
using DevHobby.GPTizza.Contracts.Services;
using DevHobby.GPTizza.Contracts.Repositories;
using DevHobby.GPTizza.Model;

namespace DevHobby.GPTizza.Services;

public class PizzaRecipeDataService : IPizzaRecipeDataService
{
    private readonly IPizzaRecipeRepository _pizzaRecipeRepository;

    public PizzaRecipeDataService(IPizzaRecipeRepository pizzaRecipeRepository)
    {
        _pizzaRecipeRepository = pizzaRecipeRepository;
    }

    public async Task<IEnumerable<PizzaRecipe>> GetAllPizzaRecipes()
    {
        return await _pizzaRecipeRepository.GetAllPizzaRecipes();
    }


    public async Task<string> GetAllPizzaRecipesAsJson()
    {
        List<PizzaRecipe> pizzaRecipes = (await _pizzaRecipeRepository.GetAllPizzaRecipes()).ToList();
        return JsonSerializer.Serialize(pizzaRecipes, Context.Default.ListPizzaRecipe);
    }
}

[JsonSerializable(typeof(List<PizzaRecipe>))]
internal partial class Context : JsonSerializerContext
{
}
