using CraftBrothers.Data;
using CraftBrothers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CraftBrothers.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> objList = _db.Products;
            foreach(var obj in objList)
            {
                obj.Category = _db.Categories.FirstOrDefault(u => u.Id == obj.CategoryId);
                obj.Brand = _db.Brands.FirstOrDefault(u => u.Id == obj.BrandId);
            };
            return View(objList);
        }
        public IActionResult Upsert(int? id)
        {
            IEnumerable<SelectListItem> CategoryDropdown = _db.Categories.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            ViewBag.CategoryDropdown = CategoryDropdown;


            IEnumerable<SelectListItem> BrandDropdown = _db.Brands.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            ViewBag.BrandDropdown = BrandDropdown;


            Product product = new Product();
            if (id == null)
            {
                return View(product);
            }
            else
            {
                product = _db.Products.Find(id);
                if(product == null)
                {
                    return NotFound();
                }
                return View(product);

            }
        }
    }
}
