using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTest.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MsJenis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    JenisName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MsJenis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrKasir",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    PointReward = table.Column<string>(type: "TEXT", nullable: false),
                    Diskon = table.Column<string>(type: "TEXT", nullable: false),
                    TotalBelanja = table.Column<string>(type: "TEXT", nullable: false),
                    TotalBayar = table.Column<string>(type: "TEXT", nullable: false),
                    TsCrt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrKasir", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MsJenis");

            migrationBuilder.DropTable(
                name: "TrKasir");
        }
    }
}
