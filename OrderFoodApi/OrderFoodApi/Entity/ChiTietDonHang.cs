using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrderFoodApi.Entity
{
    public class ChiTietDonHang
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChiTietDonHangId { get; set; }

        public int DonHangId { get; set; }

        [ForeignKey(nameof(DonHangId))]
        public DonHang DonHang { get; set; }
    }
}
