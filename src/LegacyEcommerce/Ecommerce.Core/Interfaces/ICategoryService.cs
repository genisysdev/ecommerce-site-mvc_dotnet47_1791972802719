using System.Collections.Generic;
using Ecommerce.Core.ViewModels;

namespace Ecommerce.Core.Interfaces
{
    public interface ICategoryService
    {
        List<CategoryNode> GetTree();
    }
}
