using System;
using System.Collections.Generic;
using System.Linq;
using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Models;

namespace Ecommerce.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public IEnumerable<OrderSummary> GetByEmail(string email)
        {
            using (var db = new LegacyEcommerceDbEntities())
            {
                var rows = (from o in db.Orders
                            join c in db.Customers on o.CustomerId equals c.Id
                            where c.Email == email
                            orderby o.CreatedUtc descending
                            select new { o.Id, o.CreatedUtc, o.Total }).ToList();

                return rows.Select(x => new OrderSummary
                {
                    OrderId = x.Id,
                    CreatedUtc = x.CreatedUtc,
                    Total = x.Total
                }).ToList();
            }
        }

        public int PlaceOrder(string email, string name, decimal total)
        {
            using (var db = new LegacyEcommerceDbEntities())
            {
                var customer = new Customer { Email = email, Name = name };
                db.Customers.Add(customer);
                db.SaveChanges();

                var order = new Order
                {
                    CustomerId = customer.Id,
                    CreatedUtc = DateTime.UtcNow,
                    Total = total
                };
                db.Orders.Add(order);
                db.SaveChanges();

                return order.Id;
            }
        }
    }
}
