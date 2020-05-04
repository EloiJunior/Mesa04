using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mesa04.Migrations
{
    public partial class CriaçãoDeBancoMeComoNovaTabelaNoDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Banco",
                table: "Operacao");

            migrationBuilder.AddColumn<int>(
                name: "BancoMeId",
                table: "Operacao",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BancoMe",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BancoMe", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Operacao_BancoMeId",
                table: "Operacao",
                column: "BancoMeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Operacao_BancoMe_BancoMeId",
                table: "Operacao",
                column: "BancoMeId",
                principalTable: "BancoMe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operacao_BancoMe_BancoMeId",
                table: "Operacao");

            migrationBuilder.DropTable(
                name: "BancoMe");

            migrationBuilder.DropIndex(
                name: "IX_Operacao_BancoMeId",
                table: "Operacao");

            migrationBuilder.DropColumn(
                name: "BancoMeId",
                table: "Operacao");

            migrationBuilder.AddColumn<string>(
                name: "Banco",
                table: "Operacao",
                nullable: false,
                defaultValue: "");
        }
    }
}
