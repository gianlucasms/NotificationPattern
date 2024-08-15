using NewLayeredArchitecture.Domain.Entities;

namespace NewLayeredArchitecture.Domain.Repositories;

public interface ICustomerRepository
{
    Task<Customer> GetByIdAsync(int customerId);
    Task<IEnumerable<Customer>> GetAllAsync();
}

