using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using OrderFoodApi;
using OrderFoodApi.Entity;

namespace OrderFoodApi.Migrations
{
    [DbContext(typeof(OrderFoodContext))]
    [Migration("20170401084241_m0")]
    partial class m0
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("OrderFoodApi.Entity.ChiTietDonHang", b =>
                {
                    b.Property<int>("ChiTietDonHangId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DonHangId");

                    b.Property<int>("MonAnId");

                    b.Property<int>("SoLuong");

                    b.HasKey("ChiTietDonHangId");

                    b.HasIndex("DonHangId");

                    b.HasIndex("MonAnId");

                    b.ToTable("ChiTietDonHangs");
                });

            modelBuilder.Entity("OrderFoodApi.Entity.DanhMuc", b =>
                {
                    b.Property<int>("DanhMucId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Hinh");

                    b.Property<string>("TenDanhMuc");

                    b.HasKey("DanhMucId");

                    b.ToTable("DanhMucs");
                });

            modelBuilder.Entity("OrderFoodApi.Entity.DonHang", b =>
                {
                    b.Property<int>("DonHangId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Sdt");

                    b.Property<int>("TinhTrangDonHang");

                    b.HasKey("DonHangId");

                    b.HasIndex("Sdt");

                    b.ToTable("DonHangs");
                });

            modelBuilder.Entity("OrderFoodApi.Entity.KhachHang", b =>
                {
                    b.Property<string>("Sdt")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DiaChi")
                        .IsRequired();

                    b.Property<string>("Ten")
                        .IsRequired();

                    b.HasKey("Sdt");

                    b.ToTable("KhachHangs");
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

            modelBuilder.Entity("OrderFoodApi.Entity.QuanLy", b =>
                {
                    b.Property<string>("QuanLyId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password");

                    b.HasKey("QuanLyId");

                    b.ToTable("QuanLys");
                });

            modelBuilder.Entity("OrderFoodApi.Entity.ChiTietDonHang", b =>
                {
                    b.HasOne("OrderFoodApi.Entity.DonHang", "DonHang")
                        .WithMany("ChiTietDonHangs")
                        .HasForeignKey("DonHangId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OrderFoodApi.Entity.MonAn", "MonAn")
                        .WithMany()
                        .HasForeignKey("MonAnId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OrderFoodApi.Entity.DonHang", b =>
                {
                    b.HasOne("OrderFoodApi.Entity.KhachHang", "KhachHang")
                        .WithMany()
                        .HasForeignKey("Sdt");
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
