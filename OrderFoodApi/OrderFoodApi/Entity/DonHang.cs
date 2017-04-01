using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrderFoodApi.Entity
{
    public class DonHang
    {
        public int DonHangId { get; set; }

        public TinhTrangDonHang TinhTrangDonHang { get; set; }

        [ForeignKey(nameof(Sdt))]
        public KhachHang KhachHang { get; set; }        

        public string Sdt { get; set; }

        public List<ChiTietDonHang> ChiTietDonHangs { get; set; }
    }
}
