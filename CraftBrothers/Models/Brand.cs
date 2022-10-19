using System.ComponentModel.DataAnnotations;

namespace CraftBrothers.Models
{
    public class Brand

    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        public List<Product> Products { get; set; }
    }
}
