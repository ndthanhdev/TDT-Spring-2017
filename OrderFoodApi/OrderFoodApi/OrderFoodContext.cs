using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderFoodApi.Entity;

namespace OrderFoodApi
{
    public class OrderFoodContext : DbContext
    {
        public DbSet<DanhMuc> DanhMucs { get; set; }
        public DbSet<MonAn> MonAns { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<QuanLy> QuanLys { get; set; }

        public OrderFoodContext(DbContextOptions<OrderFoodContext> options)
            : base(options)
        {
            
        }
    }
}
