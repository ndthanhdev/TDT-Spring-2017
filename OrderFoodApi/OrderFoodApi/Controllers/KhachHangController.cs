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
    public class KhachHangController : Controller
    {
        OrderFoodContext _db;
        public KhachHangController(OrderFoodContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetKhachHang(string id)
        {
            return Json(await GetKhachHangBySdt(id));
        }

        [HttpPost]
        public async Task<IActionResult> DangKyKhachHang([FromBody] KhachHang khachHang)
        {
            if (!IsKhachHangCorrect(khachHang))
            {
                return BadRequest("Thong tin sai");
            }
            else if (await GetKhachHangBySdt(khachHang.Sdt) != null)
            {
                return BadRequest("Da ton tai");
            }
            else
            {
                await InnerDangKyKhachHang(khachHang);
                return Ok();
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateKhachHang([FromBody] KhachHang khachHang)
        {
            if (!IsKhachHangCorrect(khachHang))
            {
                return BadRequest("Thong tin sai");
            }
            else if (await GetKhachHangBySdt(khachHang.Sdt) == null)
            {
                return BadRequest("Khong ton tai");
            }
            else
            {
                await InnerUpdateKhachHang(khachHang);
                return Ok();
            }

        }


        private bool IsKhachHangCorrect(KhachHang khachHang)
        {
            return !(string.IsNullOrWhiteSpace(khachHang.Sdt)
                || string.IsNullOrWhiteSpace(khachHang.Ten)
                || string.IsNullOrWhiteSpace(khachHang.DiaChi));
        }

        private async Task<KhachHang> GetKhachHangBySdt(string sdt)
        {
            return await _db.KhachHangs.FirstOrDefaultAsync(kh => kh.Sdt == sdt);
        }

        private async Task<KhachHang> InnerDangKyKhachHang(KhachHang khachHang)
        {
            await _db.KhachHangs.AddAsync(khachHang);
            await _db.SaveChangesAsync();
            return khachHang;
        }

        private async Task<KhachHang> InnerUpdateKhachHang(KhachHang khachHang)
        {
            var innerKhachHang = await _db.KhachHangs.FirstOrDefaultAsync(kh => kh.Sdt == khachHang.Sdt);
            innerKhachHang.Ten = khachHang.Ten;
            innerKhachHang.DiaChi = khachHang.DiaChi;
            await _db.SaveChangesAsync();
            return innerKhachHang;
        }
    }
}
