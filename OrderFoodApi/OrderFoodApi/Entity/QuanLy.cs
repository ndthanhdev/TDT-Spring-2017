using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderFoodApi.Entity
{
    public class QuanLy
    {
        [Key]
        public string QuanLyId { get; set; }

        public string Password { get; set; }
    }
}
