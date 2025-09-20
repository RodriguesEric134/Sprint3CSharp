using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidatosData.Migrations
{
    /// <inheritdoc />
    public partial class CriarTabelaApostaSites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "APOSTA_SITES",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR2(36)", maxLength: 36, nullable: false),
                    Nome = table.Column<string>(type: "NVARCHAR2(120)", maxLength: 120, nullable: false),
                    Url = table.Column<string>(type: "NVARCHAR2(300)", maxLength: 300, nullable: false),
                    Categoria = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, defaultValue: "Geral"),
                    NivelRisco = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false, defaultValue: "Médio"),
                    DataCadastro = table.Column<DateTime>(type: "DATE", nullable: false, defaultValueSql: "SYSDATE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APOSTA_SITES", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "UX_APOSTA_SITES_URL",
                table: "APOSTA_SITES",
                column: "Url",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "APOSTA_SITES");
        }
    }
}
