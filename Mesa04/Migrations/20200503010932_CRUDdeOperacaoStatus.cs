using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mesa04.Migrations
{
    public partial class CRUDdeOperacaoStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OperacaoStatus",
                table: "Operacao");

            migrationBuilder.AddColumn<int>(
                name: "OperacaoStatusId",
                table: "Operacao",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OperacaoStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperacaoStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Operacao_OperacaoStatusId",
                table: "Operacao",
                column: "OperacaoStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Operacao_OperacaoStatus_OperacaoStatusId",
                table: "Operacao",
                column: "OperacaoStatusId",
                principalTable: "OperacaoStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operacao_OperacaoStatus_OperacaoStatusId",
                table: "Operacao");

            migrationBuilder.DropTable(
                name: "OperacaoStatus");

            migrationBuilder.DropIndex(
                name: "IX_Operacao_OperacaoStatusId",
                table: "Operacao");

            migrationBuilder.DropColumn(
                name: "OperacaoStatusId",
                table: "Operacao");

            migrationBuilder.AddColumn<int>(
                name: "OperacaoStatus",
                table: "Operacao",
                nullable: false,
                defaultValue: 0);
        }
    }
}
