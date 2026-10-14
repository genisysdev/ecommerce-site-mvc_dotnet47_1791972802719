using System.Collections.Generic;
using Ecommerce.Core.Models;

namespace Ecommerce.Core.Interfaces
{
    public interface IOrderRepository
    {
        int PlaceOrder(string email, string name, decimal total);
        IEnumerable<OrderSummary> GetByEmail(string email);
    }
}
