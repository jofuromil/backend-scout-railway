using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendScout.Migrations
{
    /// <inheritdoc />
    public partial class CambiarDistritoPorNivelDistrito : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Distrito",
                table: "Unidades");

            migrationBuilder.DropColumn(
                name: "Distrito",
                table: "GruposScout");

            migrationBuilder.AddColumn<int>(
                name: "NivelDistritoId",
                table: "Unidades",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NivelDistritoId",
                table: "GruposScout",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "NivelesDistrito",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NivelesDistrito", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NivelDistritoUsuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "TEXT", nullable: false),
                    NivelDistritoId = table.Column<int>(type: "INTEGER", nullable: false),
                    EsAdminDistrito = table.Column<bool>(type: "INTEGER", nullable: false),
                    EsInvitadoEvento = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NivelDistritoUsuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NivelDistritoUsuario_NivelesDistrito_NivelDistritoId",
                        column: x => x.NivelDistritoId,
                        principalTable: "NivelesDistrito",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NivelDistritoUsuario_Users_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Unidades_NivelDistritoId",
                table: "Unidades",
                column: "NivelDistritoId");

            migrationBuilder.CreateIndex(
                name: "IX_GruposScout_NivelDistritoId",
                table: "GruposScout",
                column: "NivelDistritoId");

            migrationBuilder.CreateIndex(
                name: "IX_NivelDistritoUsuario_NivelDistritoId",
                table: "NivelDistritoUsuario",
                column: "NivelDistritoId");

            migrationBuilder.CreateIndex(
                name: "IX_NivelDistritoUsuario_UsuarioId",
                table: "NivelDistritoUsuario",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_GruposScout_NivelesDistrito_NivelDistritoId",
                table: "GruposScout",
                column: "NivelDistritoId",
                principalTable: "NivelesDistrito",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Unidades_NivelesDistrito_NivelDistritoId",
                table: "Unidades",
                column: "NivelDistritoId",
                principalTable: "NivelesDistrito",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GruposScout_NivelesDistrito_NivelDistritoId",
                table: "GruposScout");

            migrationBuilder.DropForeignKey(
                name: "FK_Unidades_NivelesDistrito_NivelDistritoId",
                table: "Unidades");

            migrationBuilder.DropTable(
                name: "NivelDistritoUsuario");

            migrationBuilder.DropTable(
                name: "NivelesDistrito");

            migrationBuilder.DropIndex(
                name: "IX_Unidades_NivelDistritoId",
                table: "Unidades");

            migrationBuilder.DropIndex(
                name: "IX_GruposScout_NivelDistritoId",
                table: "GruposScout");

            migrationBuilder.DropColumn(
                name: "NivelDistritoId",
                table: "Unidades");

            migrationBuilder.DropColumn(
                name: "NivelDistritoId",
                table: "GruposScout");

            migrationBuilder.AddColumn<string>(
                name: "Distrito",
                table: "Unidades",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Distrito",
                table: "GruposScout",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
