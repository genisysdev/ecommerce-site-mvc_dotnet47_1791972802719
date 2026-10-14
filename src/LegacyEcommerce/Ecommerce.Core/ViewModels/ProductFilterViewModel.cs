namespace Ecommerce.Core.ViewModels
{
    public class ProductFilterViewModel
    {
        public int? CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string SortBy { get; set; } // "price_asc", "price_desc", "name"
    }
}
