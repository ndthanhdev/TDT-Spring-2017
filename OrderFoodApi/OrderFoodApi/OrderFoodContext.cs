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

        public OrderFoodContext(DbContextOptions<OrderFoodContext> options)
            : base(options)
        {
            
        }
    }
}
