using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EAgenda.Infraestrutura.Orm.Migrations
{
    /// <inheritdoc />
    public partial class Add_TBDespesa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoriaDespesa_Categoria_CategoriasId",
                table: "CategoriaDespesa");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoriaDespesa_Despesa_DespesasId",
                table: "CategoriaDespesa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Despesa",
                table: "Despesa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoriaDespesa",
                table: "CategoriaDespesa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categoria",
                table: "Categoria");

            migrationBuilder.RenameTable(
                name: "Despesa",
                newName: "Despesas");

            migrationBuilder.RenameTable(
                name: "CategoriaDespesa",
                newName: "DespesaCategoria");

            migrationBuilder.RenameTable(
                name: "Categoria",
                newName: "Categorias");

            migrationBuilder.RenameIndex(
                name: "IX_CategoriaDespesa_DespesasId",
                table: "DespesaCategoria",
                newName: "IX_DespesaCategoria_DespesasId");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Despesas",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Despesas",
                table: "Despesas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DespesaCategoria",
                table: "DespesaCategoria",
                columns: new[] { "CategoriasId", "DespesasId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categorias",
                table: "Categorias",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DespesaCategoria_Categorias_CategoriasId",
                table: "DespesaCategoria",
                column: "CategoriasId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DespesaCategoria_Despesas_DespesasId",
                table: "DespesaCategoria",
                column: "DespesasId",
                principalTable: "Despesas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DespesaCategoria_Categorias_CategoriasId",
                table: "DespesaCategoria");

            migrationBuilder.DropForeignKey(
                name: "FK_DespesaCategoria_Despesas_DespesasId",
                table: "DespesaCategoria");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Despesas",
                table: "Despesas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DespesaCategoria",
                table: "DespesaCategoria");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categorias",
                table: "Categorias");

            migrationBuilder.RenameTable(
                name: "Despesas",
                newName: "Despesa");

            migrationBuilder.RenameTable(
                name: "DespesaCategoria",
                newName: "CategoriaDespesa");

            migrationBuilder.RenameTable(
                name: "Categorias",
                newName: "Categoria");

            migrationBuilder.RenameIndex(
                name: "IX_DespesaCategoria_DespesasId",
                table: "CategoriaDespesa",
                newName: "IX_CategoriaDespesa_DespesasId");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Despesa",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Despesa",
                table: "Despesa",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoriaDespesa",
                table: "CategoriaDespesa",
                columns: new[] { "CategoriasId", "DespesasId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categoria",
                table: "Categoria",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriaDespesa_Categoria_CategoriasId",
                table: "CategoriaDespesa",
                column: "CategoriasId",
                principalTable: "Categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriaDespesa_Despesa_DespesasId",
                table: "CategoriaDespesa",
                column: "DespesasId",
                principalTable: "Despesa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
