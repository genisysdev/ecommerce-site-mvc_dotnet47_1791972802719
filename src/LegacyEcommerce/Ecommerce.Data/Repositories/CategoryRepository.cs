using System.Collections.Generic;
using System.Linq;
using Ecommerce.Core.Interfaces;
using CoreModels = Ecommerce.Core.Models;

namespace Ecommerce.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public IEnumerable<CoreModels.Category> GetAll()
        {
            using (var db = new LegacyEcommerceDbEntities())
            {
                return db.Categories
                    .OrderBy(c => c.DisplayOrder)
                    .ToList()
                    .Select(c => new CoreModels.Category
                    {
                        Id = c.Id,
                        Name = c.Name,
                        ParentId = c.ParentId,
                        DisplayOrder = c.DisplayOrder
                    })
                    .ToList();
            }
        }
    }
}
