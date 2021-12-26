using Microsoft.EntityFrameworkCore.Migrations;

namespace WP_Proje.Migrations
{
    public partial class Sepett : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SepetDetay_Sepet_SepetId",
                table: "SepetDetay");

            migrationBuilder.DropColumn(
                name: "SiparisNo",
                table: "SepetDetay");

            migrationBuilder.AlterColumn<int>(
                name: "SepetId",
                table: "SepetDetay",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SepetDetay_Sepet_SepetId",
                table: "SepetDetay",
                column: "SepetId",
                principalTable: "Sepet",
                principalColumn: "SepetId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SepetDetay_Sepet_SepetId",
                table: "SepetDetay");

            migrationBuilder.AlterColumn<int>(
                name: "SepetId",
                table: "SepetDetay",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "SiparisNo",
                table: "SepetDetay",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_SepetDetay_Sepet_SepetId",
                table: "SepetDetay",
                column: "SepetId",
                principalTable: "Sepet",
                principalColumn: "SepetId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
