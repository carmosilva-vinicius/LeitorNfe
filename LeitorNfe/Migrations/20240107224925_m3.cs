using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeitorNfe.Migrations
{
    /// <inheritdoc />
    public partial class m3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CNPJ",
                table: "Destinatarios");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Destinatarios",
                newName: "CPF");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CPF",
                table: "Destinatarios",
                newName: "Email");

            migrationBuilder.AddColumn<string>(
                name: "CNPJ",
                table: "Destinatarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
