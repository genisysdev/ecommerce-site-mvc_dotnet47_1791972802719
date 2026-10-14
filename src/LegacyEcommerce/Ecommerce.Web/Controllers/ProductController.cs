using System.Web.Mvc;
using Ecommerce.Core.Interfaces;
using Ecommerce.Core.ViewModels;

namespace Ecommerce.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        // GET: /Product  (full page)
        public ActionResult Index(int? categoryId)
        {
            var filter = new ProductFilterViewModel { CategoryId = categoryId };
            var model = _productService.GetList(filter);
            return View(model);
        }

        // GET: /Product/Filter  (AJAX -> partial HTML fragment)
        [HttpGet]
        public ActionResult Filter(ProductFilterViewModel filter)
        {
            var model = _productService.GetList(filter);
            return PartialView("_ProductList", model);
        }

        // GET: /Product/Detail/5
        public ActionResult Detail(int id)
        {
            var model = _productService.GetDetail(id);
            if (model == null) return HttpNotFound();
            return View(model);
        }

        // Child action for the layout sidebar
        [ChildActionOnly]
        public ActionResult Sidebar()
        {
            return PartialView("_Sidebar", _categoryService.GetTree());
        }
    }
}
