using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelMate.Infrastructure.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1bd9abc0-c646-4848-b216-3685875a303b", "300b2afd-b7ea-413d-aee4-20f444159418", "Admin", "admin" },
                    { "e571299a-b482-4d73-becc-33e93c51ad05", "96026e09-f269-4065-8f54-136785dab507", "TravelGuru", "travelguru" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "The old continent - the place for culture, history, cuisine and so much more.", "Europe" },
                    { 2, "The land of diversity - nature, technology and so much more", "North America" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Description", "Name", "RegionId" },
                values: new object[] { 1, "The best place to live in. Lots of mountains, beautiful seaside, lovely food and amazing people.", "Bulgaria", 1 });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Description", "Name", "RegionId" },
                values: new object[] { 2, "History, culture, fine dining, crystal-clear water, endless beaches - you can find all that here.", "Greece", 1 });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Description", "Name", "RegionId" },
                values: new object[] { 3, "The country of endless possibilities, what you can think of - you can find it here", "USA", 2 });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "CountryId", "Email", "EmailConfirmed", "FirstName", "Information", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePictureUrl", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "0dfd8f1e-fba5-4db7-82a7-0ba2e5cc6a09", 0, new DateTime(2022, 12, 7, 13, 22, 59, 255, DateTimeKind.Local).AddTicks(9322), "81d211c6-e1de-4716-baf4-cd72f0fadbe1", 3, "stanka@mail.com", false, "Stanka", "I love to travel to all the beautiful cities in the world and meet all the great people there!", false, "Petrova", false, null, "stanka@mail.com", "stanka", "AQAAAAEAACcQAAAAEJRxOirl8EfuWjKywCo0lJvSYpH4ZnhqFXpNGmn941fp9nn6JOyrD5TKhUCvb+U3OA==", null, false, "https://pub-static.fotor.com/assets/projects/pages/d5bdd0513a0740a8a38752dbc32586d0/fotor-03d1a91a0cec4542927f53c87e0599f6.jpg", "423ce27c-ce2c-4752-b3a5-0b8c35126ddc", false, "Stanka" },
                    { "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", 0, new DateTime(2022, 12, 7, 13, 22, 59, 254, DateTimeKind.Local).AddTicks(4955), "ffb7bd9c-5778-4130-a976-519305bc6bdc", 2, "gosho@mail.com", false, "Gosho", "I am looking forward to visiting all the oceans of the world!", false, "Ivanov", false, null, "gosho@mail.com", "gosho", "AQAAAAEAACcQAAAAELIh8KvS4CYOhmsOsZ0UQHqCxap1M2QyzUVXfK/FvPRnUtNOs9Rgwqa7Qb1imY2PWQ==", null, false, "https://thumbs.dreamstime.com/b/profile-picture-vector-perfect-social-media-other-web-use-125320944.jpg", "39776bb6-a71e-4b72-a0e0-a53ad074e8c9", false, "Gosho" },
                    { "9717154f-b1cc-44e4-a23b-7dcfdea11eb1", 0, new DateTime(2022, 12, 7, 13, 22, 59, 257, DateTimeKind.Local).AddTicks(3793), "1ba7ed31-9a08-4e05-8fe8-1430452bf828", 1, "admin@mail.com", false, "Ivan", "I want to create the best plarform for travellers in the world!", false, "Ivanov", false, null, "admin@mail.com", "admin", "AQAAAAEAACcQAAAAEHzOY++9kMARk/+pQU56UOru6Xmo00hYuyq4vOmFe0H2eLMmyTIGaUldJfO+lbrHCg==", null, false, "https://thumbs.dreamstime.com/b/profile-picture-vector-perfect-social-media-other-web-use-125320944.jpg", "d5bb0c44-a76d-48f9-b2e7-7c1eb75a6f73", false, "Admin" },
                    { "dea12856-c198-4129-b3f3-b893d8395082", 0, new DateTime(2022, 12, 7, 13, 22, 59, 252, DateTimeKind.Local).AddTicks(9905), "fce8523a-ffca-4082-92b6-f95da52282ed", 1, "stamat@mail.com", false, "Stamat", "I love to travel in the mountains of Europe", false, "Petrov", false, null, "stamat@mail.com", "stamat", "AQAAAAEAACcQAAAAEOnT6ZU+vkJ63fgoRkjEtrJNOtf77cpM35HPctDsImnNAXY9a3eINayYpdceLyeQAg==", null, false, "https://i.pinimg.com/originals/d9/56/9b/d9569bbed4393e2ceb1af7ba64fdf86a.jpg", "bb34bab7-3c78-475b-af5a-fbfc120ca164", false, "Stamat" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "e571299a-b482-4d73-becc-33e93c51ad05", "0dfd8f1e-fba5-4db7-82a7-0ba2e5cc6a09" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1bd9abc0-c646-4848-b216-3685875a303b", "9717154f-b1cc-44e4-a23b-7dcfdea11eb1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e571299a-b482-4d73-becc-33e93c51ad05", "0dfd8f1e-fba5-4db7-82a7-0ba2e5cc6a09" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1bd9abc0-c646-4848-b216-3685875a303b", "9717154f-b1cc-44e4-a23b-7dcfdea11eb1" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1bd9abc0-c646-4848-b216-3685875a303b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e571299a-b482-4d73-becc-33e93c51ad05");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0dfd8f1e-fba5-4db7-82a7-0ba2e5cc6a09");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9717154f-b1cc-44e4-a23b-7dcfdea11eb1");

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
