using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CsharpClicker.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBoosts_AspNetUsers_ApplicationUserId",
                table: "UserBoosts");

            migrationBuilder.DropIndex(
                name: "IX_UserBoosts_ApplicationUserId",
                table: "UserBoosts");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "UserBoosts");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Boosts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "UserBoosts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Boosts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserBoosts_ApplicationUserId",
                table: "UserBoosts",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBoosts_AspNetUsers_ApplicationUserId",
                table: "UserBoosts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
