using DevHobby.GPTizza.Model;

namespace DevHobby.GPTizza.Contracts.Services;

public interface IOrderDataService
{
    Task<IEnumerable<Order>> GetAllOrdersForMonthAndYear(int month, int year);
    Task<IEnumerable<Order>> GetAllOrdersForPizzaForMonthAndYear(int month, int year, int pizzaId);
}
