namespace CraftBrothers.Models.ViewModels
{
    public class HomeVM
    
    {

        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Brand>Brands { get;set; }
    }

}
