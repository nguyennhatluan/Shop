using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Model.Models
{
    [Table("ProductTags")]
    public class ProductTag
    {
        [Key,Column(Order=0)]
        public int ProductID { set; get; }
        [Key, Column(Order = 1)]
        public int TagID { set; get; }
    }
}
