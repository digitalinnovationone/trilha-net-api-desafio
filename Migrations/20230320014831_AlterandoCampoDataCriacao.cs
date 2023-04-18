using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrilhaApiDesafio.Migrations
{
    public partial class AlterandoCampoDataCriacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCriacao",
                table: "Tarefa",
                type: "SMALLDATETIME",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "SMALLDATETIME",
                oldDefaultValue: new DateTime(2023, 3, 20, 1, 18, 36, 42, DateTimeKind.Utc).AddTicks(427));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCriacao",
                table: "Tarefa",
                type: "SMALLDATETIME",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 20, 1, 18, 36, 42, DateTimeKind.Utc).AddTicks(427),
                oldClrType: typeof(DateTime),
                oldType: "SMALLDATETIME",
                oldMaxLength: 60);
        }
    }
}
