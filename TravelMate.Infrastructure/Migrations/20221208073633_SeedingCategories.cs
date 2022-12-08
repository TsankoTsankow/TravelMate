using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelMate.Infrastructure.Migrations
{
    public partial class SeedingCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Here you can share all your experiences in the nature", "Nature" },
                    { 2, "A place for all the cultural experiences", "Culture" },
                    { 3, "For the lovers of fine dining", "Cuisine" },
                    { 4, "The most beautiful and fascinating cities of the world", "Urban" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

           
        }
    }
}
