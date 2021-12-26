using Microsoft.EntityFrameworkCore.Migrations;

namespace WP_Proje.Migrations
{
    public partial class Sepet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sepet",
                columns: table => new
                {
                    SepetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SepetFiyat = table.Column<int>(type: "int", nullable: false),
                    KullaniciId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sepet", x => x.SepetId);
                    table.ForeignKey(
                        name: "FK_Sepet_AspNetUsers_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SepetDetay",
                columns: table => new
                {
                    SepetDetayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiparisNo = table.Column<int>(type: "int", nullable: false),
                    SepetId = table.Column<int>(type: "int", nullable: true),
                    UrunNo = table.Column<int>(type: "int", nullable: false),
                    CicekId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SepetDetay", x => x.SepetDetayId);
                    table.ForeignKey(
                        name: "FK_SepetDetay_Cicekler_CicekId",
                        column: x => x.CicekId,
                        principalTable: "Cicekler",
                        principalColumn: "CicekId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SepetDetay_Sepet_SepetId",
                        column: x => x.SepetId,
                        principalTable: "Sepet",
                        principalColumn: "SepetId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sepet_KullaniciId",
                table: "Sepet",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_SepetDetay_CicekId",
                table: "SepetDetay",
                column: "CicekId");

            migrationBuilder.CreateIndex(
                name: "IX_SepetDetay_SepetId",
                table: "SepetDetay",
                column: "SepetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SepetDetay");

            migrationBuilder.DropTable(
                name: "Sepet");
        }
    }
}
