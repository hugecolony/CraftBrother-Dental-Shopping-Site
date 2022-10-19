using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CraftBrothers.Models
{
    public class Category

    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DisplayName("Description")]
        [Required]
        public string Description { get; set; }
        
        public List<Product> Products { get; set; }
    }
}
