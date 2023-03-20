using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrilhaApiDesafio.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tarefa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "NVARCHAR(20)", maxLength: 20, nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR(30)", maxLength: 30, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "SMALLDATETIME", nullable: false, defaultValue: new DateTime(2023, 3, 20, 1, 18, 36, 42, DateTimeKind.Utc).AddTicks(427)),
                    Status = table.Column<string>(type: "NVARCHAR(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarefa", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tarefa_Name",
                table: "Tarefa",
                column: "Titulo",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tarefa");
        }
    }
}
