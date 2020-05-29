using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mesa04.Migrations
{
    public partial class CriaçãoCrudMoedaEstrangeira : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MeId",
                table: "Operacao",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Me",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    Codigo = table.Column<int>(nullable: false),
                    Abreviacao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Me", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Operacao_MeId",
                table: "Operacao",
                column: "MeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Operacao_Me_MeId",
                table: "Operacao",
                column: "MeId",
                principalTable: "Me",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operacao_Me_MeId",
                table: "Operacao");

            migrationBuilder.DropTable(
                name: "Me");

            migrationBuilder.DropIndex(
                name: "IX_Operacao_MeId",
                table: "Operacao");

            migrationBuilder.DropColumn(
                name: "MeId",
                table: "Operacao");
        }
    }
}
