using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SHG.Migrations
{
    public partial class UserLik_for_bulletin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VoterId",
                table: "Bulletins");

            migrationBuilder.AddColumn<string>(
                name: "UserLik",
                table: "Bulletins",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserLik",
                table: "Bulletins");

            migrationBuilder.AddColumn<int>(
                name: "VoterId",
                table: "Bulletins",
                nullable: false,
                defaultValue: 0);
        }
    }
}
