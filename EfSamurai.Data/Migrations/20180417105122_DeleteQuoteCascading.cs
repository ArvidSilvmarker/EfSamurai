using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EfSamurai.Data.Migrations
{
    public partial class DeleteQuoteCascading : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BattleLog_Battles_BattleId",
                table: "BattleLog");

            migrationBuilder.DropForeignKey(
                name: "FK_Quote_Samurais_SamuraiId",
                table: "Quote");

            migrationBuilder.AddForeignKey(
                name: "FK_BattleLog_Battles_BattleId",
                table: "BattleLog",
                column: "BattleId",
                principalTable: "Battles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quote_Samurais_SamuraiId",
                table: "Quote",
                column: "SamuraiId",
                principalTable: "Samurais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BattleLog_Battles_BattleId",
                table: "BattleLog");

            migrationBuilder.DropForeignKey(
                name: "FK_Quote_Samurais_SamuraiId",
                table: "Quote");

            migrationBuilder.AddForeignKey(
                name: "FK_BattleLog_Battles_BattleId",
                table: "BattleLog",
                column: "BattleId",
                principalTable: "Battles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quote_Samurais_SamuraiId",
                table: "Quote",
                column: "SamuraiId",
                principalTable: "Samurais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
