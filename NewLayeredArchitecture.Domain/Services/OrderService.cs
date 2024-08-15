using NewLayeredArchitecture.Domain.Entities;
using NewLayeredArchitecture.Domain.Repositories;

namespace NewLayeredArchitecture.Domain.Services;

public class OrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task PlaceOrder(Order order)
    {
        if (order.Items.Count == 0)
        {
            throw new InvalidOperationException("Order must have at least one item.");
        }

        await _orderRepository.AddAsync(order);
    }
}

