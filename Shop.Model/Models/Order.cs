using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Model.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int ID { set; get; }
        [Required]
        public string CustomerName { set; get; }
        [Required]
        public string CustomerAddress { set; get; }
        public string CustomerEmail { set; get; }
        public string CustomerMobile { set; get; }
        public DateTime CreatedDate { set; get; }
        public string CreatedBy { set; get; }
        public string PaymentMethod { set; get; }
        public string PaymentStatus { set; get; }
        public bool Status { set; get; }
    }
}
