using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderFoodApi.Entity
{
    public class KhachHang
    {
        [Key]
        public string Sdt { get; set; }

        [Required]
        public string Ten { get; set; }

        [Required]
        public string DiaChi { get; set; }
    }
}
