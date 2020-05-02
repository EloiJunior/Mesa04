using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mesa04.Migrations
{
    public partial class ClasseTipoRegistoNacionalMigradaDoEnumParaModelComScafoldingDeControllerEViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TipoRegistroNacional",
                table: "Cliente",
                newName: "TipoRegistroNacionalId");

            migrationBuilder.CreateTable(
                name: "TipoRegistroNacional",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoRegistroNacional", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_TipoRegistroNacionalId",
                table: "Cliente",
                column: "TipoRegistroNacionalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_TipoRegistroNacional_TipoRegistroNacionalId",
                table: "Cliente",
                column: "TipoRegistroNacionalId",
                principalTable: "TipoRegistroNacional",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_TipoRegistroNacional_TipoRegistroNacionalId",
                table: "Cliente");

            migrationBuilder.DropTable(
                name: "TipoRegistroNacional");

            migrationBuilder.DropIndex(
                name: "IX_Cliente_TipoRegistroNacionalId",
                table: "Cliente");

            migrationBuilder.RenameColumn(
                name: "TipoRegistroNacionalId",
                table: "Cliente",
                newName: "TipoRegistroNacional");
        }
    }
}
