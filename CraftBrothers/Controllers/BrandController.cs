using CraftBrothers.Data;
using CraftBrothers.Models;
using Microsoft.AspNetCore.Mvc;

namespace CraftBrothers.Controllers
{
    public class BrandController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BrandController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Brand> objList = _db.Brands;
            return View(objList);
        }

        //Get create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Brand brand)
        {
            //if (ModelState.IsValid) { 
            //    _db.Brands.Add(brand);
            //    _db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //    return View(brand);
            _db.Brands.Add(brand);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var obj = _db.Brands.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Brand brand)
        {
            //     if (ModelState.IsValid)
            //    {
            //        _db.Brands.Update(brand);
            //        _db.SaveChanges();
            //        return RedirectToAction("Index");
            //            return View(brand);
            //    }
            _db.Brands.Update(brand);
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
            var obj = _db.Brands.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(Brand brand)
        {
                _db.Brands.Remove(brand);
                _db.SaveChanges();
                return RedirectToAction("Index");

        }

    }
}
