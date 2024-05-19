using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHint.Data.Migrations
{
    /// <inheritdoc />
    public partial class trocaDatanascimento_datacadastro_addEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataNascimento",
                table: "Cliente",
                newName: "DataCadastro");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Cliente",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Cliente");

            migrationBuilder.RenameColumn(
                name: "DataCadastro",
                table: "Cliente",
                newName: "DataNascimento");
        }
    }
}
