using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AltaRegistrosVilla3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidad", "Detalle", "FA", "FUM", "ImageUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "ameno", "Ningun lugar", new DateTime(2023, 11, 22, 14, 24, 32, 656, DateTimeKind.Local).AddTicks(6089), null, "", 1000, "En ningun Lugar", 100, 1001.21m },
                    { 2, "ameno", "Ningun lugar", new DateTime(2023, 11, 22, 14, 24, 32, 656, DateTimeKind.Local).AddTicks(6100), null, "", 1000, "En ningun Lugar", 100, 1001.21m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
