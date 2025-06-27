using Microsoft.AspNetCore.Mvc;
using YasserSports.Services;

namespace YasserSports.Controllers
{
    public class StoreController : Controller
    {
        private readonly ApplicationDbContext context;

        public StoreController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index(string searchQuery = "", int page = 1)
        {
            int pageSize = 8; // Number of products per page
            var productsQuery = context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                productsQuery = productsQuery.Where(p => p.Name.Contains(searchQuery) ||
                                                         p.Brand.Contains(searchQuery) ||
                                                         p.Category.Contains(searchQuery));
            }

            var totalProducts = productsQuery.Count(); // Total number of matching products

            var products = productsQuery
                .OrderByDescending(p => p.Id) // Keep the ordering
                .Skip((page - 1) * pageSize) // Skip products for previous pages
                .Take(pageSize) // Take only the products for the current page
                .ToList();

            ViewBag.Products = products;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            ViewBag.SearchQuery = searchQuery;

            return View();
        }
        public IActionResult Details(int id)
        {
            var products = context.Products.Find(id);
            return View(products);
        }

    }
}
