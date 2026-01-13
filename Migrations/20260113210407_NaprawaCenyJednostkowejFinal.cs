using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CukierniaAdamMus.Migrations
{
    /// <inheritdoc />
    public partial class NaprawaCenyJednostkowejFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Opis",
                table: "Produkty",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<decimal>(
                name: "CenaJednostkowa",
                table: "PozycjeZamowien",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CenaJednostkowa",
                table: "PozycjeZamowien");

            migrationBuilder.AlterColumn<string>(
                name: "Opis",
                table: "Produkty",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
