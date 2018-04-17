using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EfSamurai.Data.Migrations
{
    public partial class RequiredBattleLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BattleLog_BattleId",
                table: "BattleLog");

            migrationBuilder.AlterColumn<int>(
                name: "BattleId",
                table: "BattleLog",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BattleLog_BattleId",
                table: "BattleLog",
                column: "BattleId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BattleLog_BattleId",
                table: "BattleLog");

            migrationBuilder.AlterColumn<int>(
                name: "BattleId",
                table: "BattleLog",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_BattleLog_BattleId",
                table: "BattleLog",
                column: "BattleId",
                unique: true,
                filter: "[BattleId] IS NOT NULL");
        }
    }
}
