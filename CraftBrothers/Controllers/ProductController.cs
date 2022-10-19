using CraftBrothers.Data;
using CraftBrothers.Models;
using CraftBrothers.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CraftBrothers.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(ApplicationDbContext db,IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
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
        [HttpGet]
        public IActionResult Upsert(int? id)
        {

            //IEnumerable<SelectListItem> CategoryDropdown = _db.Categories.Select(i => new SelectListItem
            //{
            //    Text = i.Name,
            //    Value = i.Id.ToString()
            //});
            //ViewBag.CategoryDropdown = CategoryDropdown;


            //IEnumerable<SelectListItem> BrandDropdown = _db.Brands.Select(i => new SelectListItem
            //{
            //    Text = i.Name,
            //    Value = i.Id.ToString()
            //});
            //ViewBag.BrandDropdown = BrandDropdown;


            //Product product = new Product();
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategorySelectList = _db.Categories.Select(i => new SelectListItem
                {

                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                BrandSelectList = _db.Brands.Select(i => new SelectListItem
                {

                    Text = i.Name,
                    Value = i.Id.ToString()
                })


            };
             
            if (id == null)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product = _db.Products.Find(id.GetValueOrDefault());
                if(productVM.Product == null)
                {
                    return NotFound();
                }
                return View(productVM);

            }
        }
        // Post upsert
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM)
        {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (productVM.Product.BrandId == 0)
                {
                    //Creating
                    string upload = webRootPath + WC.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    productVM.Product.Image = fileName + extension;
                    _db.Products.Add(productVM.Product);
                }

                else
                {
                    //updating
                    var objFromDb = _db.Products.AsNoTracking().FirstOrDefault(u => u.Name == productVM.Product.Name);
                    if (files.Count() > 0)
                    {
                        string upload = webRootPath + WC.ImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        var oldFile = Path.Combine(upload, objFromDb.Image);
                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }
                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }
                        //new image 
                        productVM.Product.Image = fileName + extension;
                    }
                    else
                    {
                        //keep image as it is 
                        //other changes
                        productVM.Product.Image = objFromDb.Image;
                    }
                    _db.Products.Update(productVM.Product);
                }
                 _db.SaveChanges();
                return RedirectToAction("Index");
            
        }

        //Get-delte

        //GET - DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product product = _db.Products.Include(u => u.Category).FirstOrDefault(u => u.Id == id);
            
            //Product product = _db.Products.FirstOrDefault(u => u.Id == id, includeProperties: "Category,Brand");
            //product.Category = _db.Category.Find(product.CategoryId);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        //POST - DELETE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Products.Find(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }

            string upload = _webHostEnvironment.WebRootPath + WC.ImagePath;
            var oldFile = Path.Combine(upload, obj.Image);

            if (System.IO.File.Exists(oldFile))
            {
                System.IO.File.Delete(oldFile);
            }


            _db.Remove(obj);
            _db.SaveChanges();
            //            TempData[WC.Success] = "Action completed successfully";
            return RedirectToAction("Index");


        }

    }

}

