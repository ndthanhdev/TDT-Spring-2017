using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderFoodApi.Entity;

namespace OrderFoodApi.Controllers
{
    [Produces("application/json")]
    public class MonAnController : Controller
    {
        private readonly OrderFoodContext _context;

        public MonAnController(OrderFoodContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("{id}")]
        public JsonResult GetMonAns(int id)
        {
            var danhMuc = _context.DanhMucs.Include(dm => dm.MonAns).FirstOrDefault(dm => dm.DanhMucId == id);
            return Json(danhMuc.MonAns);
        }

        [HttpPost]
        public async Task<IActionResult> AddMonAn([FromBody] AddMonAnData data)
        {
            if (await _context.QuanLys.FirstOrDefaultAsync(
                    ql => ql.QuanLyId == data.QuanLy.QuanLyId && ql.Password == data.QuanLy.Password) == null)
            {
                return Unauthorized();
            }
            var innerDanhMuc = await _context.DanhMucs.FirstOrDefaultAsync(dm => dm.DanhMucId == data.MonAn.DanhMucId);
            if (innerDanhMuc == null)
            {
                return NotFound("khong co danh muc nay");
            }
            await _context.AddAsync(data.MonAn);
            await _context.SaveChangesAsync();
            return Json(data.MonAn);
        }

        public class AddMonAnData
        {
            public QuanLy QuanLy { get; set; }
            public MonAn MonAn { get; set; }
        }
    }
}