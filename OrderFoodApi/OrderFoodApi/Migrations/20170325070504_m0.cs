using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderFoodApi.Migrations
{
    public partial class m0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DanhMucs",
                columns: table => new
                {
                    DanhMucId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Hinh = table.Column<string>(nullable: true),
                    TenDanhMuc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMucs", x => x.DanhMucId);
                });

            migrationBuilder.CreateTable(
                name: "MonAns",
                columns: table => new
                {
                    MonAnId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DanhMucId = table.Column<int>(nullable: false),
                    Gia = table.Column<int>(nullable: false),
                    Hinh = table.Column<string>(nullable: true),
                    MoTa = table.Column<string>(nullable: true),
                    TenMonAn = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonAns", x => x.MonAnId);
                    table.ForeignKey(
                        name: "FK_MonAns_DanhMucs_DanhMucId",
                        column: x => x.DanhMucId,
                        principalTable: "DanhMucs",
                        principalColumn: "DanhMucId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MonAns_DanhMucId",
                table: "MonAns",
                column: "DanhMucId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonAns");

            migrationBuilder.DropTable(
                name: "DanhMucs");
        }
    }
}
