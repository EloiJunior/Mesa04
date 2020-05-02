using Microsoft.EntityFrameworkCore.Migrations;

namespace Mesa04.Migrations
{
    public partial class RegistroNacionalDeIntParaString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RegistroNacional",
                table: "Cliente",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RegistroNacional",
                table: "Cliente",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
