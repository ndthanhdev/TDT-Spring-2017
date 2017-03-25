using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderFoodApi.Controllers
{
    public class DanhMucController : Controller
    {
        private readonly OrderFoodContext _context;

        public DanhMucController(OrderFoodContext context)
        {
            _context = context;
        }

        public JsonResult GetDanhMucs()
        {
            var danhMucs = _context.DanhMucs.ToArray();
            return Json(danhMucs);
        }


    }
}
