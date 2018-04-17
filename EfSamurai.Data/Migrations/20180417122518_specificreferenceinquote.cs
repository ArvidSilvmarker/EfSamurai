using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EfSamurai.Data.Migrations
{
    public partial class specificreferenceinquote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quote_Samurais_SamuraiId",
                table: "Quote");

            migrationBuilder.AlterColumn<int>(
                name: "SamuraiId",
                table: "Quote",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

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
                name: "FK_Quote_Samurais_SamuraiId",
                table: "Quote");

            migrationBuilder.AlterColumn<int>(
                name: "SamuraiId",
                table: "Quote",
                nullable: true,
                oldClrType: typeof(int));

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
