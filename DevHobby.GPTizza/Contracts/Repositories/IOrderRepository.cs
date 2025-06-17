using DevHobby.GPTizza.Model;

namespace DevHobby.GPTizza.Contracts.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllOrdersForMonthAndYear(int month, int year);
    Task<IEnumerable<Order>> GetAllOrdersForPizzaForMonthAndYear(int month, int year, int pizzaId);
}
