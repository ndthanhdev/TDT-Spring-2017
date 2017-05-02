using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderFoodApi.Entity;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderFoodApi.Controllers
{
    public class DanhMucController : Controller
    {
        private readonly OrderFoodContext _db;

        public DanhMucController(OrderFoodContext context)
        {
            _db = context;
        }

        [HttpGet]
        public JsonResult GetDanhMucs()
        {
            var danhMucs = _db.DanhMucs.ToArray();
            return Json(danhMucs);
        }

        [HttpPost]
        public async Task<IActionResult> AddDanhMuc(DanhMucAddDanhMucData data)
        {
            if (await _db.DanhMucs.FirstOrDefaultAsync(dm => dm.TenDanhMuc == data.TenDanhMuc) != null)
            {
                return BadRequest("Ten danh muc da ton tai");
            }
            var danhMuc = new DanhMuc()
            {
                TenDanhMuc = data.TenDanhMuc,
                Hinh = data.Hinh
            };
            await _db.DanhMucs.AddAsync(danhMuc);
            await _db.SaveChangesAsync();
            return Json(danhMuc);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDanhMuc(DanhMuUpdateDanhMucData data)
        {
            var danhMucIndb = await _db.DanhMucs.FirstOrDefaultAsync(dm => dm.DanhMucId == data.DanhMucId);
            if ((await _db.DanhMucs.FirstOrDefaultAsync(dm => dm.DanhMucId == data.DanhMucId)) == null)
            {
                return BadRequest("Ten danh muc khong ton tai");
            }
            if (await _db.QuanLys.FirstOrDefaultAsync(
                    ql => ql.QuanLyId == data.QuanLy.QuanLyId && ql.Password == data.QuanLy.Password) == null)
            {
                return Unauthorized();
            }
            danhMucIndb.Hinh = data.Hinh;
            danhMucIndb.TenDanhMuc = data.TenDanhMuc;
            await _db.SaveChangesAsync();
            return Json(danhMucIndb);
        }


        public class DanhMucAddDanhMucData
        {
            public QuanLy QuanLy { get; set; }
            public string TenDanhMuc { get; set; }
            public string Hinh { get; set; }
        }

        public class DanhMuUpdateDanhMucData
        {
            public QuanLy QuanLy { get; set; }
            public int DanhMucId { get; set; }
            public string TenDanhMuc { get; set; }
            public string Hinh { get; set; }
        }


    }
}
