using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderFoodApi.Entity;

namespace OrderFoodApi.Controllers
{
    [Produces("application/json")]
    public class SeedController : Controller
    {
        private readonly OrderFoodContext _context;

        public SeedController(OrderFoodContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            await InnerSeedDanhMucMonAn();


            return Ok("Seeded");
        }

        private async Task InnerSeedDanhMucMonAn()
        {
            // 1
            var danhMuc1 = new DanhMuc()
            {
                TenDanhMuc = "Thức uống",
                Hinh = "icon_drink_menu"
            };
            await _context.DanhMucs.AddAsync(danhMuc1);
            await _context.MonAns.AddAsync(new MonAn()
            {
                TenMonAn = "Cà phê sữa",
                Gia = 10000,
                MoTa = "Cà phê từ ngũ cốc rang cháy + sữa hết date bao ung thư",
                DanhMuc = danhMuc1
            });
            await _context.MonAns.AddAsync(new MonAn()
            {
                TenMonAn = "Sữa tươi",
                Gia = 10000,
                MoTa = "Sữa chứa melamin",
                DanhMuc = danhMuc1
            });



            // 2
            var danhMuc2 = new DanhMuc()
            {
                TenDanhMuc = "Đồ ăn",
                Hinh = "icon_food_menu"
            };
            await _context.AddAsync(danhMuc2);
            await _context.MonAns.AddAsync(new MonAn()
            {
                TenMonAn = "Bánh mì ốp la",
                Gia = 10000,
                MoTa = "Bánh mì mốc + trứng made in China"
                ,
                DanhMuc = danhMuc2
            });
            await _context.MonAns.AddAsync(new MonAn()
            {
                TenMonAn = "Mì tôm",
                Gia = 10000,
                MoTa = "Mì thoy ko có tôm đâu ahihi",
                DanhMuc = danhMuc2
            });

            // 3
            var danhMuc3 = new DanhMuc()
            {
                TenDanhMuc = "Lẩu",
                Hinh = "icon_lau_menu"
            };
            await _context.AddAsync(danhMuc3);
            await _context.MonAns.AddAsync(new MonAn()
            {
                TenMonAn = "Lẩu hải sản",
                Gia = 10000,
                MoTa = "lẩu từ Hải sản tươi sống Vũng Áng",
                DanhMuc = danhMuc3
            });

            await _context.SaveChangesAsync();
        }



    }
}