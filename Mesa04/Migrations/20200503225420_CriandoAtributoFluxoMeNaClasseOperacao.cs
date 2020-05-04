using Microsoft.EntityFrameworkCore.Migrations;

namespace Mesa04.Migrations
{
    public partial class CriandoAtributoFluxoMeNaClasseOperacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Fluxo",
                table: "Operacao",
                newName: "FluxoMn");

            migrationBuilder.AddColumn<int>(
                name: "FluxoMe",
                table: "Operacao",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FluxoMe",
                table: "Operacao");

            migrationBuilder.RenameColumn(
                name: "FluxoMn",
                table: "Operacao",
                newName: "Fluxo");
        }
    }
}
