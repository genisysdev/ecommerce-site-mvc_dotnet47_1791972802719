using System.Collections.Generic;
using Ecommerce.Core.Models;

namespace Ecommerce.Core.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<OrderSummary> GetHistory(string email);
    }
}
