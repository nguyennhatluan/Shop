using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Model.Models
{
    [Table("ContactDetails")]
    public class ContactDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }
        [StringLength (250)]
        [Required(ErrorMessage ="Tên không được trống")]
        public string Name { set; get; }
        [StringLength(50,ErrorMessage ="Số điện thoại không vượt quá 50 ký tự")]
        public string Phone { set; get; }
        [StringLength(250)]
        public string Email { set; get; }
        [StringLength(250)]
        public string Website { set; get; }
        [StringLength(250)]
        public string Address { set; get; }
        public string Other { set; get; }
        public double? Lat { set; get; }
        public double? Lng { set; get; }
        public bool? Status { set; get; }

    }
}
