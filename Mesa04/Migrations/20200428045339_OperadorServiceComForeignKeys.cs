using Microsoft.EntityFrameworkCore.Migrations;

namespace Mesa04.Migrations
{
    public partial class OperadorServiceComForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operacao_Cliente_ClienteId",
                table: "Operacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Operacao_Operador_OperadorId",
                table: "Operacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Operador_Departamento_DepartamentoId",
                table: "Operador");

            migrationBuilder.AlterColumn<int>(
                name: "DepartamentoId",
                table: "Operador",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OperadorId",
                table: "Operacao",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClienteId",
                table: "Operacao",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoOperacaoId",
                table: "Operacao",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Operacao_Cliente_ClienteId",
                table: "Operacao",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Operacao_Operador_OperadorId",
                table: "Operacao",
                column: "OperadorId",
                principalTable: "Operador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Operador_Departamento_DepartamentoId",
                table: "Operador",
                column: "DepartamentoId",
                principalTable: "Departamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operacao_Cliente_ClienteId",
                table: "Operacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Operacao_Operador_OperadorId",
                table: "Operacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Operador_Departamento_DepartamentoId",
                table: "Operador");

            migrationBuilder.DropColumn(
                name: "TipoOperacaoId",
                table: "Operacao");

            migrationBuilder.AlterColumn<int>(
                name: "DepartamentoId",
                table: "Operador",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "OperadorId",
                table: "Operacao",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ClienteId",
                table: "Operacao",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Operacao_Cliente_ClienteId",
                table: "Operacao",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Operacao_Operador_OperadorId",
                table: "Operacao",
                column: "OperadorId",
                principalTable: "Operador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Operador_Departamento_DepartamentoId",
                table: "Operador",
                column: "DepartamentoId",
                principalTable: "Departamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
