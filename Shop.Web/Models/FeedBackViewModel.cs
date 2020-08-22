using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Web.Models
{
    public class FeedBackViewModel
    {
        
        public int ID { set; get; }

        [Required(ErrorMessage ="Phải nhập tên")]
        public string Name { set; get; }

        [Required(ErrorMessage = "Phải nhập Email")]
        public string Email { set; get; }

        [Required(ErrorMessage = "Phải nhập nội dung")]
        public string Message { set; get; }

        public DateTime CreatedDate { set; get; }

        
        public bool Status { set; get; }
    }
}