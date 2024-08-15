using System.Collections.Generic;
using System.Threading.Tasks;
using NewLayeredArchitecture.Application.Notifications;
using NewLayeredArchitecture.Domain.Entities;
using NewLayeredArchitecture.Domain.Repositories;

namespace NewLayeredArchitecture.Application.Services
{
    public class OrderAppService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly INotificationHandler _notificationHandler;

        public OrderAppService(IOrderRepository orderRepository, INotificationHandler notificationHandler)
        {
            _orderRepository = orderRepository;
            _notificationHandler = notificationHandler;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task AddOrderAsync(Order order)
        {
            if (order == null)
            {
                _notificationHandler.AddNotification("Order", "Order cannot be null.");
                return;
            }

            if (order.CustomerId <= 0)
            {
                _notificationHandler.AddNotification("Order", "Invalid CustomerId.");
            }

            var customerExists = await _orderRepository.CustomerExistsAsync(order.CustomerId);
            if (!customerExists)
            {
                _notificationHandler.AddNotification("Order", "Customer does not exist.");
            }

            if (order.Items == null || !order.Items.Any())
            {
                _notificationHandler.AddNotification("Order", "Order must have at least one item.");
            }

            foreach (var item in order.Items)
            {
                if (item.OrderId != order.OrderId)
                {
                    _notificationHandler.AddNotification("OrderItem", $"OrderItem with ID {item.OrderItemId} has mismatched OrderId.");
                }
            }

            if (_notificationHandler.HasNotifications())
            {
                return;
            }

            await _orderRepository.AddAsync(order);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            if (order == null)
            {
                _notificationHandler.AddNotification("Order", "Order cannot be null.");
                return;
            }

            var existingOrder = await _orderRepository.GetByIdAsync(order.OrderId);
            if (existingOrder == null)
            {
                _notificationHandler.AddNotification("Order", "Order not found.");
                return;
            }

            if (order.CustomerId <= 0)
            {
                _notificationHandler.AddNotification("Order", "Invalid CustomerId.");
            }

            var customerExists = await _orderRepository.CustomerExistsAsync(order.CustomerId);
            if (!customerExists)
            {
                _notificationHandler.AddNotification("Order", "Customer does not exist.");
            }

            if (order.Items == null || !order.Items.Any())
            {
                _notificationHandler.AddNotification("Order", "Order must have at least one item.");
            }

            foreach (var item in order.Items)
            {
                if (item.OrderId != order.OrderId)
                {
                    _notificationHandler.AddNotification("OrderItem", $"OrderItem with ID {item.OrderItemId} has mismatched OrderId.");
                }

            }

            if (_notificationHandler.HasNotifications())
            {
                return;
            }

            await _orderRepository.UpdateAsync(order);
        }


        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                _notificationHandler.AddNotification("Order", "Order not found.");
                return false;
            }

            if (_notificationHandler.HasNotifications())
            {
                return false;
            }

            try
            {
                await _orderRepository.DeleteAsync(order);
                return true;
            }
            catch (Exception ex)
            {
                _notificationHandler.AddNotification("Order", $"An error occurred while deleting the order: {ex.Message}");
                return false;
            }
        }
    }
}
