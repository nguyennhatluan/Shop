using Shop.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Model.Models
{
    [Table("PostCategories")]
    public class PostCategory:Auditable
    {
        [Key]
        public int ID { set; get; }
        [Required]
        public string Name { set; get; }

        public string Alias { set; get; }
        public string Description { set; get; }
        public int ParentID { set; get; }
        public int DisplayOrder { set; get; }
        public string Image { set; get; }
        public bool? HomeFlag { set; get; }

    }
}
