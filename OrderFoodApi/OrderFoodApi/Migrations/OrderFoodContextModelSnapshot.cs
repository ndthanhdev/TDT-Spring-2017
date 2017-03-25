using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using OrderFoodApi;

namespace OrderFoodApi.Migrations
{
    [DbContext(typeof(OrderFoodContext))]
    partial class OrderFoodContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("OrderFoodApi.Entity.DanhMuc", b =>
                {
                    b.Property<int>("DanhMucId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Hinh");

                    b.Property<string>("TenDanhMuc");

                    b.HasKey("DanhMucId");

                    b.ToTable("DanhMucs");
                });

            modelBuilder.Entity("OrderFoodApi.Entity.MonAn", b =>
                {
                    b.Property<int>("MonAnId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DanhMucId");

                    b.Property<int>("Gia");

                    b.Property<string>("Hinh");

                    b.Property<string>("MoTa");

                    b.Property<string>("TenMonAn");

                    b.HasKey("MonAnId");

                    b.HasIndex("DanhMucId");

                    b.ToTable("MonAns");
                });

            modelBuilder.Entity("OrderFoodApi.Entity.MonAn", b =>
                {
                    b.HasOne("OrderFoodApi.Entity.DanhMuc", "DanhMuc")
                        .WithMany("MonAns")
                        .HasForeignKey("DanhMucId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
