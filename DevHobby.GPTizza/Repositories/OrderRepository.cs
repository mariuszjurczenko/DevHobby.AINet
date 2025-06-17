using DevHobby.GPTizza.Contracts.Repositories;
using DevHobby.GPTizza.Data;
using DevHobby.GPTizza.Model;
using Microsoft.EntityFrameworkCore;

namespace DevHobby.GPTizza.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public OrderRepository(IDbContextFactory<ApplicationDbContext> DbFactory)
    {
        _applicationDbContext = DbFactory.CreateDbContext();
    }

    public async Task<IEnumerable<Order>> GetAllOrdersForMonthAndYear(int month, int year)
    {
        return await _applicationDbContext.Orders
            .Where(o => o.OrderPlaced.Month == month && o.OrderPlaced.Year == year)
            .Include(o => o.OrderLines)
            .ThenInclude(o => o.Pizza)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetAllOrdersForPizzaForMonthAndYear(int month, int year, int pizzaId)
    {
        return await _applicationDbContext.Orders
            .Where(o => o.OrderPlaced.Month == month && o.OrderPlaced.Year == year && o.OrderLines
            .Any(p => p.PizzaId == pizzaId))
            .Include(o => o.OrderLines)
            .ThenInclude(o => o.Pizza)
            .ToListAsync();
    }
}
