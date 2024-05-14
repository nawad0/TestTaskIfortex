using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _dbContext;
    public UserService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<User> GetUser()
    {
        return await _dbContext.Users.AsNoTracking()
            .Where(u => u.Orders.Any(o => o.CreatedAt.Year == 2003 && o.Status == OrderStatus.Delivered))
            .OrderByDescending(u => u.Orders.Sum(o => o.Quantity)).FirstOrDefaultAsync() ?? throw new Exception("User not found");
    }
    public async Task<List<User>> GetUsers()
    {
        return await _dbContext.Users.AsNoTracking()
            .Where(u => u.Orders.Any(o => o.CreatedAt.Year == 2010 && o.Status == OrderStatus.Paid)).ToListAsync();
    }
}