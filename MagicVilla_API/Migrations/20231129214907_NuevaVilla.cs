using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class NuevaVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Tarifa",
                table: "Villas",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.CreateTable(
                name: "NuemroVilla",
                columns: table => new
                {
                    Numero = table.Column<int>(type: "int", nullable: false),
                    VillaId = table.Column<int>(type: "int", nullable: false),
                    Detalle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FUM = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NuemroVilla", x => x.Numero);
                    table.ForeignKey(
                        name: "FK_NuemroVilla_Villas_VillaId",
                        column: x => x.VillaId,
                        principalTable: "Villas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "FA",
                value: new DateTime(2023, 11, 29, 15, 49, 7, 528, DateTimeKind.Local).AddTicks(4069));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "FA",
                value: new DateTime(2023, 11, 29, 15, 49, 7, 528, DateTimeKind.Local).AddTicks(4086));

            migrationBuilder.CreateIndex(
                name: "IX_NuemroVilla_VillaId",
                table: "NuemroVilla",
                column: "VillaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NuemroVilla");

            migrationBuilder.AlterColumn<decimal>(
                name: "Tarifa",
                table: "Villas",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "FA",
                value: new DateTime(2023, 11, 22, 14, 24, 32, 656, DateTimeKind.Local).AddTicks(6089));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "FA",
                value: new DateTime(2023, 11, 22, 14, 24, 32, 656, DateTimeKind.Local).AddTicks(6100));
        }
    }
}
