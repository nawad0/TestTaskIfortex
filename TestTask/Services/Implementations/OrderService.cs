using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _dbContext;
    public OrderService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Order> GetOrder()
    {
        return await _dbContext.Orders.AsNoTracking().Where(o => o.Quantity > 1)
            .OrderByDescending(o => o.CreatedAt).FirstOrDefaultAsync() ?? throw new Exception("Order not found");
    }

    public async Task<List<Order>> GetOrders()
    {
        return await _dbContext.Orders.AsNoTracking().Where(u => u.User.Status == UserStatus.Active)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }
}