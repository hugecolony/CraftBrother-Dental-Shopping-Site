using Microsoft.AspNetCore.Mvc.Rendering;

namespace CraftBrothers.Models.ViewModels
{
    public class ProductVM
    {
        public  Product Product { get; set; }
        public  IEnumerable<SelectListItem> CategorySelectList { get; set; }
        public  IEnumerable<SelectListItem> BrandSelectList { get; set; }
    }
}
