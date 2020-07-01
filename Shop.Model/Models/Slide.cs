using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Model.Models
{
    [Table("Slides")]
    public class Slide
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Image { set; get; }
        public string Url { set; get; }
        public int DisplayOrder { set; get; }
        public bool? Status { set; get; }
    }
}
