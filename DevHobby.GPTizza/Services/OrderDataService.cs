using DevHobby.GPTizza.Contracts.Repositories;
using DevHobby.GPTizza.Contracts.Services;
using DevHobby.GPTizza.Model;

namespace DevHobby.GPTizza.Services;

public class OrderDataService : IOrderDataService
{
    private readonly IOrderRepository _orderRepository;

    public OrderDataService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<Order>> GetAllOrdersForMonthAndYear(int month, int year)
    {
        return await _orderRepository.GetAllOrdersForMonthAndYear(month, year);
    }

    public async Task<IEnumerable<Order>> GetAllOrdersForPizzaForMonthAndYear(int month, int year, int pizzaId)
    {
        return await _orderRepository.GetAllOrdersForPizzaForMonthAndYear(month, year, pizzaId);
    }
}