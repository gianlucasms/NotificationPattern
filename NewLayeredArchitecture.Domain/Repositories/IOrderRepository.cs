using NewLayeredArchitecture.Domain.Entities;

namespace NewLayeredArchitecture.Domain.Repositories;

public interface IOrderRepository
{
    Task<Order> GetByIdAsync(int orderId);
    Task<IEnumerable<Order>> GetAllAsync();
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
    Task DeleteAsync(Order order);
    Task<bool> CustomerExistsAsync(int customerId); 
}

