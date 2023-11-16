using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace learn.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataforDifficultiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("721f7b45-2e23-4ba5-a8bc-7b047481fd91"), "Medium" },
                    { new Guid("8324d18e-f798-499d-8fba-a0a80e925412"), "Hard" },
                    { new Guid("cee58370-8dc7-4f3b-b1a3-0a961be18f72"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("18c9dfa7-f68a-4344-85d8-aac12c6572cb"), "MDR", "Madura", "https://picsum.photos/id/3/400/300" },
                    { new Guid("44731059-8385-4260-8317-472341e74576"), "GRS", "Gresik", "https://picsum.photos/id/1/400/300" },
                    { new Guid("55f065e4-9959-4d54-b468-f3b8688f0d63"), "KDR", "Kediri", "https://picsum.photos/id/2/400/300" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("721f7b45-2e23-4ba5-a8bc-7b047481fd91"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("8324d18e-f798-499d-8fba-a0a80e925412"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("cee58370-8dc7-4f3b-b1a3-0a961be18f72"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("18c9dfa7-f68a-4344-85d8-aac12c6572cb"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("44731059-8385-4260-8317-472341e74576"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("55f065e4-9959-4d54-b468-f3b8688f0d63"));
        }
    }
}
