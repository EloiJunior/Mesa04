using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mesa04.Migrations
{
    public partial class CriadoCrudCompletoDeTipoOperacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoOperacao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoOperacao", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Operacao_TipoOperacaoId",
                table: "Operacao",
                column: "TipoOperacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Operacao_TipoOperacao_TipoOperacaoId",
                table: "Operacao",
                column: "TipoOperacaoId",
                principalTable: "TipoOperacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operacao_TipoOperacao_TipoOperacaoId",
                table: "Operacao");

            migrationBuilder.DropTable(
                name: "TipoOperacao");

            migrationBuilder.DropIndex(
                name: "IX_Operacao_TipoOperacaoId",
                table: "Operacao");
        }
    }
}
