using Microsoft.EntityFrameworkCore.Migrations;

namespace Mesa04.Migrations
{
    public partial class JuncaoDeMeComOperacaoAindaTesteParcialFaltandoCriarMeNaTabelaOperacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operacao_Me_MeId",
                table: "Operacao");

            migrationBuilder.AlterColumn<int>(
                name: "MeId",
                table: "Operacao",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Operacao_Me_MeId",
                table: "Operacao",
                column: "MeId",
                principalTable: "Me",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operacao_Me_MeId",
                table: "Operacao");

            migrationBuilder.AlterColumn<int>(
                name: "MeId",
                table: "Operacao",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Operacao_Me_MeId",
                table: "Operacao",
                column: "MeId",
                principalTable: "Me",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
