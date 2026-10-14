using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Unity;
using Unity.Exceptions;

namespace Ecommerce.Web.Infrastructure
{
    // Bridges the Unity container to MVC's IDependencyResolver so controllers
    // get constructor injection.
    public class UnityDependencyResolver : IDependencyResolver
    {
        private readonly IUnityContainer _container;

        public UnityDependencyResolver(IUnityContainer container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return Enumerable.Empty<object>();
            }
        }
    }
}
