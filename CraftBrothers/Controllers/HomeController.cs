using CraftBrothers.Data;
using CraftBrothers.Models;
using CraftBrothers.Models.ViewModel;
using CraftBrothers.Models.ViewModels;
using CraftBrothers.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CraftBrothers.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        public HomeController(ILogger<HomeController> logger,ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM
            {
                Products = _db.Products.Include(u => u.Category).Include(u => u.Brand),
                Categories = _db.Categories,
                Brands = _db.Brands

                //populate list of categories
            };
            return View(homeVM);
        }

        public IActionResult Details(int id)
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                //if have values then retrive and Add
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            DetailsVM DetailsVM = new DetailsVM()
            {
                //Return only first record
                Product = _db.Products.Include(u => u.Category).Include(u => u.Brand)
                .Where(u => u.Id == id).FirstOrDefault(),
                ExistsInCart = false
            };
            foreach (var item in shoppingCartList)
            {
                if (item.ProductId == id)
                {
                    // item exists in shopping cart
                    DetailsVM.ExistsInCart = true;
                }
            }
            return View(DetailsVM);
        }
        [HttpPost,ActionName("Details")]

        public IActionResult DetailsPost(int id)
        {

            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                //if have values then retrive and Add
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }
            //if empty directly add to shopping cart
            shoppingCartList.Add(new ShoppingCart { ProductId = id});
            HttpContext.Session.Set(WC.SessionCart, shoppingCartList);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult RemoveFromCart(int id)
        {
            //List of shopping cart

            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                //if have values then retrive and Add
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }
            // item to remove 
            var itemtoRemove = shoppingCartList.SingleOrDefault(u => u.ProductId == id);
            if (itemtoRemove != null)
            {
                shoppingCartList.Remove(itemtoRemove);
            }

            //if empty directly add to shopping cart
            //shoppingCartList.Add(new ShoppingCart { ProductId = id });
            HttpContext.Session.Set(WC.SessionCart, shoppingCartList);
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}