using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Model.Models
{
    [Table("Footers")]
    public class Footer
    {
        [Key]
        public int ID { set; get; }

        [Required]
        public string Content { set; get; }
    }
}