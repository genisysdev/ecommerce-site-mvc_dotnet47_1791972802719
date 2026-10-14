using System.Collections.Generic;

namespace Ecommerce.Core.ViewModels
{
    public class CategoryNode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CategoryNode> Children { get; set; } = new List<CategoryNode>();
    }
}
