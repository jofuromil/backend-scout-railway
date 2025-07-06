using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendScout.Migrations
{
    /// <inheritdoc />
    public partial class AgregarValidacionObjetivosYEspecialidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DirigenteValidadorId",
                table: "RequisitoCumplidos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEnvioDistrito",
                table: "RegistrosGestion",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DirigenteValidadorId",
                table: "ObjetivosSeleccionados",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaValidacion",
                table: "ObjetivosSeleccionados",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequisitoCumplidos_DirigenteValidadorId",
                table: "RequisitoCumplidos",
                column: "DirigenteValidadorId");

            migrationBuilder.CreateIndex(
                name: "IX_ObjetivosSeleccionados_DirigenteValidadorId",
                table: "ObjetivosSeleccionados",
                column: "DirigenteValidadorId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ObjetivosSeleccionados_Users_DirigenteValidadorId",
                table: "ObjetivosSeleccionados");

            migrationBuilder.DropForeignKey(
                name: "FK_RequisitoCumplidos_Users_DirigenteValidadorId",
                table: "RequisitoCumplidos");

            migrationBuilder.DropIndex(
                name: "IX_RequisitoCumplidos_DirigenteValidadorId",
                table: "RequisitoCumplidos");

            migrationBuilder.DropIndex(
                name: "IX_ObjetivosSeleccionados_DirigenteValidadorId",
                table: "ObjetivosSeleccionados");

            migrationBuilder.DropColumn(
                name: "DirigenteValidadorId",
                table: "RequisitoCumplidos");

            migrationBuilder.DropColumn(
                name: "FechaEnvioDistrito",
                table: "RegistrosGestion");

            migrationBuilder.DropColumn(
                name: "DirigenteValidadorId",
                table: "ObjetivosSeleccionados");

            migrationBuilder.DropColumn(
                name: "FechaValidacion",
                table: "ObjetivosSeleccionados");
        }
    }
}
