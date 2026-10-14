using System.Collections.Generic;
using Ecommerce.Core.Models;

namespace Ecommerce.Core.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
    }
}
