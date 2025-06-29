using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendScout.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCamposEstáticosRegistroGestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CI",
                table: "RegistrosGestion",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DistritoNombre",
                table: "RegistrosGestion",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaNacimiento",
                table: "RegistrosGestion",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GrupoNombre",
                table: "RegistrosGestion",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombreCompleto",
                table: "RegistrosGestion",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rama",
                table: "RegistrosGestion",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnidadNombre",
                table: "RegistrosGestion",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CI",
                table: "RegistrosGestion");

            migrationBuilder.DropColumn(
                name: "DistritoNombre",
                table: "RegistrosGestion");

            migrationBuilder.DropColumn(
                name: "FechaNacimiento",
                table: "RegistrosGestion");

            migrationBuilder.DropColumn(
                name: "GrupoNombre",
                table: "RegistrosGestion");

            migrationBuilder.DropColumn(
                name: "NombreCompleto",
                table: "RegistrosGestion");

            migrationBuilder.DropColumn(
                name: "Rama",
                table: "RegistrosGestion");

            migrationBuilder.DropColumn(
                name: "UnidadNombre",
                table: "RegistrosGestion");
        }
    }
}
