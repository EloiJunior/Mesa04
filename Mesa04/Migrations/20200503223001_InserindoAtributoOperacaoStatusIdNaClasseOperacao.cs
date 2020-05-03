using Microsoft.EntityFrameworkCore.Migrations;

namespace Mesa04.Migrations
{
    public partial class InserindoAtributoOperacaoStatusIdNaClasseOperacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operacao_OperacaoStatus_OperacaoStatusId",
                table: "Operacao");

            migrationBuilder.AlterColumn<int>(
                name: "OperacaoStatusId",
                table: "Operacao",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Operacao_OperacaoStatus_OperacaoStatusId",
                table: "Operacao",
                column: "OperacaoStatusId",
                principalTable: "OperacaoStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operacao_OperacaoStatus_OperacaoStatusId",
                table: "Operacao");

            migrationBuilder.AlterColumn<int>(
                name: "OperacaoStatusId",
                table: "Operacao",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Operacao_OperacaoStatus_OperacaoStatusId",
                table: "Operacao",
                column: "OperacaoStatusId",
                principalTable: "OperacaoStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
