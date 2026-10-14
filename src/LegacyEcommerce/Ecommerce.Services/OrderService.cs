using System.Collections.Generic;
using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Models;

namespace Ecommerce.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orders;

        public OrderService(IOrderRepository orders)
        {
            _orders = orders;
        }

        public IEnumerable<OrderSummary> GetHistory(string email)
        {
            return _orders.GetByEmail(email);
        }
    }
}
