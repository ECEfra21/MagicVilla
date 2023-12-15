using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class NuevaVilla2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NuemroVilla_Villas_VillaId",
                table: "NuemroVilla");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NuemroVilla",
                table: "NuemroVilla");

            migrationBuilder.RenameTable(
                name: "NuemroVilla",
                newName: "NumeroVilla");

            migrationBuilder.RenameIndex(
                name: "IX_NuemroVilla_VillaId",
                table: "NumeroVilla",
                newName: "IX_NumeroVilla_VillaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NumeroVilla",
                table: "NumeroVilla",
                column: "Numero");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "FA",
                value: new DateTime(2023, 11, 29, 15, 51, 48, 580, DateTimeKind.Local).AddTicks(5168));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "FA",
                value: new DateTime(2023, 11, 29, 15, 51, 48, 580, DateTimeKind.Local).AddTicks(5184));

            migrationBuilder.AddForeignKey(
                name: "FK_NumeroVilla_Villas_VillaId",
                table: "NumeroVilla",
                column: "VillaId",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NumeroVilla_Villas_VillaId",
                table: "NumeroVilla");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NumeroVilla",
                table: "NumeroVilla");

            migrationBuilder.RenameTable(
                name: "NumeroVilla",
                newName: "NuemroVilla");

            migrationBuilder.RenameIndex(
                name: "IX_NumeroVilla_VillaId",
                table: "NuemroVilla",
                newName: "IX_NuemroVilla_VillaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NuemroVilla",
                table: "NuemroVilla",
                column: "Numero");

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

            migrationBuilder.AddForeignKey(
                name: "FK_NuemroVilla_Villas_VillaId",
                table: "NuemroVilla",
                column: "VillaId",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
