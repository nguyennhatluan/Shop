using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Model.Models
{
    [Table("PostTags")]
    public class PostTag
    {
        public int PostID { set; get; }
        public int TagID { set; get; }
    }
}
