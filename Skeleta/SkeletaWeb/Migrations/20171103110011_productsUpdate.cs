using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SkeletaWeb.Migrations
{
    public partial class productsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "AppProducts");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "AppProducts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "AppProducts");

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "AppProducts",
                nullable: true);
        }
    }
}
