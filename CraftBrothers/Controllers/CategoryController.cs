using CraftBrothers.Data;
using CraftBrothers.Models;
using Microsoft.AspNetCore.Mvc;

namespace CraftBrothers.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db=db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objList = _db.Categories;
            return View(objList);
        }
        
        //Get create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
          
                _db.Categories.Add(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            if(Id == null || Id == 0)
            {
                return NotFound();
            }
            var obj = _db.Categories.Find(Id);
            if(obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
                _db.Categories.Update(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var obj = _db.Categories.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(Category category)
        {
                _db.Categories.Remove(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
        }
    }

}
