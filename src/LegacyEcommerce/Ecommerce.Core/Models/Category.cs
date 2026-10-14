namespace Ecommerce.Core.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public int DisplayOrder { get; set; }
    }
}
