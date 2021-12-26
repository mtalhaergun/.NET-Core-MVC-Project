using Microsoft.EntityFrameworkCore.Migrations;

namespace WP_Proje.Migrations
{
    public partial class sepetdetayekle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bilgi",
                table: "SepetDetay",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Fiyat",
                table: "SepetDetay",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Isim",
                table: "SepetDetay",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Resim",
                table: "SepetDetay",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Stok",
                table: "SepetDetay",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bilgi",
                table: "SepetDetay");

            migrationBuilder.DropColumn(
                name: "Fiyat",
                table: "SepetDetay");

            migrationBuilder.DropColumn(
                name: "Isim",
                table: "SepetDetay");

            migrationBuilder.DropColumn(
                name: "Resim",
                table: "SepetDetay");

            migrationBuilder.DropColumn(
                name: "Stok",
                table: "SepetDetay");
        }
    }
}
