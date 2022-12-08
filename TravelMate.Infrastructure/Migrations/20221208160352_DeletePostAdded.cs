using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelMate.Infrastructure.Migrations
{
    public partial class DeletePostAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Posts");

           
        }
    }
}
