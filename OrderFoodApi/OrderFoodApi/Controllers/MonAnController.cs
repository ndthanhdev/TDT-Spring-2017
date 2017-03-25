using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public JsonResult GetMonAns(int id)
        {
            var danhMuc = _context.DanhMucs.Include(dm => dm.MonAns).FirstOrDefault(dm => dm.DanhMucId == id);
            return Json(danhMuc.MonAns);
        }
    }
}