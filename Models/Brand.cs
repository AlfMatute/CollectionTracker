using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollectionTrackerMVC.Models
{
    public class Brand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BrandId { get; set; }
        [Required]
        [MaxLength(100)]
        public string BrandName { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
    }
}
