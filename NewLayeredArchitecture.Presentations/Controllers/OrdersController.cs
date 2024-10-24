using Microsoft.AspNetCore.Mvc;
using NewLayeredArchitecture.Application.Notifications;
using NewLayeredArchitecture.Application.Services;
using NewLayeredArchitecture.Domain.Entities;
using System.Threading.Tasks;

namespace NewLayeredArchitecture.Presentation.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OrderAppService _orderAppService;
        private readonly INotificationHandler _notificationHandler;

        public OrdersController(OrderAppService orderAppService, INotificationHandler notificationHandler)
        {
            _orderAppService = orderAppService;
            _notificationHandler = notificationHandler;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _orderAppService.GetAllOrdersAsync(); 
            return View(orders);
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderAppService.GetOrderByIdAsync(id); 
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order)
        {
            if (!ModelState.IsValid)
            {
                return View(order);
            }

            await _orderAppService.AddOrderAsync(order);

            if (_notificationHandler.HasNotifications())
            {
                foreach (var notification in _notificationHandler.GetNotifications())
                {
                    ModelState.AddModelError(notification.Key, notification.Message);
                }
                return View(order);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var order = await _orderAppService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(order);
            }

            await _orderAppService.UpdateOrderAsync(order); 

            if (_notificationHandler.HasNotifications())
            {
                foreach (var notification in _notificationHandler.GetNotifications())
                {
                    ModelState.AddModelError(notification.Key, notification.Message);
                }
                return View(order);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var order = await _orderAppService.GetOrderByIdAsync(id); 
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _orderAppService.DeleteOrderAsync(id);

            if (_notificationHandler.HasNotifications())
            {
                foreach (var notification in _notificationHandler.GetNotifications())
                {
                    ModelState.AddModelError(notification.Key, notification.Message);
                }
                return RedirectToAction(nameof(Delete), new { id });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
