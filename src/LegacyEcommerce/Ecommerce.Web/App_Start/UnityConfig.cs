using System.Web.Mvc;
using Ecommerce.Core.Interfaces;
using Ecommerce.Data.Repositories;
using Ecommerce.Services;
using Ecommerce.Web.Infrastructure;
using Unity;

namespace Ecommerce.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<ICategoryRepository, CategoryRepository>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<ICategoryService, CategoryService>();
            container.RegisterType<ICartService, CartService>();
            container.RegisterType<IOrderRepository, OrderRepository>();
            container.RegisterType<ICheckoutService, CheckoutService>();
            container.RegisterType<IOrderService, OrderService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
