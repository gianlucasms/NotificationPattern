using Microsoft.EntityFrameworkCore;
using NewLayeredArchitecture.Domain.Entities;
using NewLayeredArchitecture.Domain.Repositories;
using NewLayeredArchitecture.Infra.Context;

namespace NewLayeredArchitecture.Infra.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _context.Orders
            .Include(o => o.Items) 
            .Include(o => o.Customer) 
            .ToListAsync(); 
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        return await _context.Orders
            .Include(o => o.Items) 
            .Include(o => o.Customer) 
            .FirstOrDefaultAsync(o => o.OrderId == id); 
    }

    public async Task AddAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Order order)
    {
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> CustomerExistsAsync(int customerId)
    {
        return await _context.Customers.AnyAsync(c => c.CustomerId == customerId);
    }
}

