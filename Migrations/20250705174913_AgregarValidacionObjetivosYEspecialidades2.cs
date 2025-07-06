using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendScout.Migrations
{
    /// <inheritdoc />
    public partial class AgregarValidacionObjetivosYEspecialidades2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ObjetivosSeleccionados_Users_DirigenteValidadorId",
                table: "ObjetivosSeleccionados");

            migrationBuilder.DropForeignKey(
                name: "FK_RequisitoCumplidos_Users_DirigenteValidadorId",
                table: "RequisitoCumplidos");

            migrationBuilder.AddForeignKey(
                name: "FK_ObjetivosSeleccionados_Users_DirigenteValidadorId",
                table: "ObjetivosSeleccionados",
                column: "DirigenteValidadorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequisitoCumplidos_Users_DirigenteValidadorId",
                table: "RequisitoCumplidos",
                column: "DirigenteValidadorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ObjetivosSeleccionados_Users_DirigenteValidadorId",
                table: "ObjetivosSeleccionados");

            migrationBuilder.DropForeignKey(
                name: "FK_RequisitoCumplidos_Users_DirigenteValidadorId",
                table: "RequisitoCumplidos");

            migrationBuilder.AddForeignKey(
                name: "FK_ObjetivosSeleccionados_Users_DirigenteValidadorId",
                table: "ObjetivosSeleccionados",
                column: "DirigenteValidadorId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RequisitoCumplidos_Users_DirigenteValidadorId",
                table: "RequisitoCumplidos",
                column: "DirigenteValidadorId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
