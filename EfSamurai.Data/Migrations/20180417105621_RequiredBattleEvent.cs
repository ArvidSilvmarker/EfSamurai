using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EfSamurai.Data.Migrations
{
    public partial class RequiredBattleEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BattleEvent_BattleLog_BattleLogId",
                table: "BattleEvent");

            migrationBuilder.AlterColumn<int>(
                name: "BattleLogId",
                table: "BattleEvent",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BattleEvent_BattleLog_BattleLogId",
                table: "BattleEvent",
                column: "BattleLogId",
                principalTable: "BattleLog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BattleEvent_BattleLog_BattleLogId",
                table: "BattleEvent");

            migrationBuilder.AlterColumn<int>(
                name: "BattleLogId",
                table: "BattleEvent",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_BattleEvent_BattleLog_BattleLogId",
                table: "BattleEvent",
                column: "BattleLogId",
                principalTable: "BattleLog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
