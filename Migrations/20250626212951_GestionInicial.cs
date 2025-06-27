using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendScout.Migrations
{
    /// <inheritdoc />
    public partial class GestionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gestiones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaCierre = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EstaActiva = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gestiones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegistrosGestion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "TEXT", nullable: false),
                    GestionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AprobadoGrupo = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaAprobadoGrupo = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EnviadoADistrito = table.Column<bool>(type: "INTEGER", nullable: false),
                    AprobadoDistrito = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaAprobadoDistrito = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EnviadoANacional = table.Column<bool>(type: "INTEGER", nullable: false),
                    AprobadoNacional = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaAprobadoNacional = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosGestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrosGestion_Gestiones_GestionId",
                        column: x => x.GestionId,
                        principalTable: "Gestiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistrosGestion_Users_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosGestion_GestionId",
                table: "RegistrosGestion",
                column: "GestionId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosGestion_UsuarioId",
                table: "RegistrosGestion",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistrosGestion");

            migrationBuilder.DropTable(
                name: "Gestiones");
        }
    }
}
