using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace berber.Migrations
{
    /// <inheritdoc />
    public partial class AddHizmetTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Hizmetler_HizmetId",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_HizmetId",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "HizmetId",
                table: "Randevular");

            migrationBuilder.AddColumn<string>(
                name: "Aciklama",
                table: "Hizmetler",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aciklama",
                table: "Hizmetler");

            migrationBuilder.AddColumn<int>(
                name: "HizmetId",
                table: "Randevular",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_HizmetId",
                table: "Randevular",
                column: "HizmetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Hizmetler_HizmetId",
                table: "Randevular",
                column: "HizmetId",
                principalTable: "Hizmetler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
