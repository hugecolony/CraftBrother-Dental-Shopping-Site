namespace CraftBrothers.Models.ViewModel
{
    public class DetailsVM
    {
        //ctor //contructor
        public DetailsVM()
        {
            Product = new Product();                            
        }

        public Product Product { get; set; }
        public bool ExistsInCart { get; set; }
    }
}
