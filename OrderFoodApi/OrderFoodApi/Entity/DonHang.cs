using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrderFoodApi.Entity
{
    public class DonHang
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DonHangId { get; set; }

        [ForeignKey(nameof(Sdt))]
        public KhachHang KhachHang { get; set; }

        public string Sdt { get; set; }

        public TinhTrangDonHang TinhTrangDonHang { get; set; }

        public List<ChiTietDonHang> ChiTietDonHangs { get; set; }
    }
}
