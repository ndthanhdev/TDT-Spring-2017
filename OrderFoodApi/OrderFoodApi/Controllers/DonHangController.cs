using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderFoodApi.Entity;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderFoodApi.Controllers
{
    public class DonHangController : Controller
    {
        OrderFoodContext _db;
        public DonHangController(OrderFoodContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> DatHang([FromBody] DonHangData data)
        {
            if (data.ChiTietDonHangDatas.Count <= 0)
            {
                return BadRequest("Qua it");
            }
            else if (await GetKhachHangBySdt(data.Sdt) == null)
            {
                return BadRequest("Khach hang khong ton tai");
            }
            else
            {
                var donHang = new DonHang()
                {
                    Sdt = data.Sdt,
                    TinhTrangDonHang = TinhTrangDonHang.ChoXuLy,
                    ChiTietDonHangs = new List<ChiTietDonHang>()
                };
                List<Task> tasks = new List<Task>();

                foreach (var ctdh in data.ChiTietDonHangDatas)
                {
                    if (ctdh.SoLuong <= 0)
                    {
                        return BadRequest("So luong phai lon hon 0");
                    }
                    else
                    {
                        var monAn = await GetMonAnById(ctdh.MonAnId);
                        if (monAn == null)
                        {
                            return BadRequest("Mon an khong ton tai");
                        }
                        // mon an ton tai
                        donHang.ChiTietDonHangs.Add(new ChiTietDonHang()
                        {
                            MonAnId = ctdh.MonAnId,
                            SoLuong = ctdh.SoLuong,
                            DonGia = monAn.Gia
                        });
                    }
                }

                await _db.DonHangs.AddAsync(donHang);
                await _db.SaveChangesAsync();

                return Json(donHang);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetDonHang(int id)
        {
            return Json(await _db.DonHangs.Include(dh => dh.ChiTietDonHangs).FirstOrDefaultAsync(dh => dh.DonHangId == id));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTinhTrangDonHang([FromBody] DonHangUpdateTinhTrangDonHangData data)
        {
            if(await _db.QuanLys.FirstOrDefaultAsync(ql=>ql.QuanLyId==data.QuanLy.QuanLyId && ql.Password == data.QuanLy.Password) == null)
            {
                return BadRequest("Khong co quyen");
            }
            var donHangInDb = await _db.DonHangs.FirstOrDefaultAsync(dh => dh.DonHangId == data.DonHangId);
            if (donHangInDb == null)
            {
                return BadRequest("Don hang khong ton tai");
            }
            else
            {
                donHangInDb.TinhTrangDonHang = data.TinhTrangMoi;
                await _db.SaveChangesAsync();
                return Json(donHangInDb);
            }
        }

        private async Task<KhachHang> GetKhachHangBySdt(string sdt)
        {
            return await _db.KhachHangs.FirstOrDefaultAsync(kh => kh.Sdt == sdt);
        }

        private async Task<MonAn> GetMonAnById(int id)
        {
            return await _db.MonAns.FirstOrDefaultAsync(ma => ma.MonAnId == id);
        }

        public class DonHangData
        {
            public List<ChiTietDonHangData> ChiTietDonHangDatas { get; set; }
            public string Sdt { get; set; }
        }

        public class ChiTietDonHangData
        {
            public int MonAnId { get; set; }
            public int SoLuong { get; set; }
        }

        public class DonHangUpdateTinhTrangDonHangData
        {
            public QuanLy QuanLy { get; set; }
            public int DonHangId { get; set; }
            public TinhTrangDonHang TinhTrangMoi { get; set; }
        }
    }
}
