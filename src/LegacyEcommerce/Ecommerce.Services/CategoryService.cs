using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using Ecommerce.Core.Interfaces;
using Ecommerce.Core.ViewModels;

namespace Ecommerce.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categories;
        private const string CacheKey = "category_tree";

        public CategoryService(ICategoryRepository categories)
        {
            _categories = categories;
        }

        public List<CategoryNode> GetTree()
        {
            var cache = MemoryCache.Default;
            if (cache.Get(CacheKey) is List<CategoryNode> cached)
                return cached;

            var all = _categories.GetAll().ToList();
            var nodes = all.ToDictionary(
                c => c.Id,
                c => new CategoryNode { Id = c.Id, Name = c.Name });

            var roots = new List<CategoryNode>();
            foreach (var c in all)
            {
                if (c.ParentId.HasValue && nodes.ContainsKey(c.ParentId.Value))
                    nodes[c.ParentId.Value].Children.Add(nodes[c.Id]);
                else
                    roots.Add(nodes[c.Id]);
            }

            cache.Set(CacheKey, roots,
                new CacheItemPolicy { AbsoluteExpiration = System.DateTimeOffset.UtcNow.AddMinutes(10) });
            return roots;
        }
    }
}
