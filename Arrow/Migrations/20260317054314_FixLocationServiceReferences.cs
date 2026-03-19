using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arrow.Migrations
{
    /// <inheritdoc />
    public partial class FixLocationServiceReferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationService");

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceId",
                table: "Locations",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_ServiceId",
                table: "Locations",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Services_ServiceId",
                table: "Locations",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Services_ServiceId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_ServiceId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Locations");

            migrationBuilder.CreateTable(
                name: "LocationService",
                columns: table => new
                {
                    AvailableLocationsId = table.Column<int>(type: "integer", nullable: false),
                    ServicesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationService", x => new { x.AvailableLocationsId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_LocationService_Locations_AvailableLocationsId",
                        column: x => x.AvailableLocationsId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationService_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocationService_ServicesId",
                table: "LocationService",
                column: "ServicesId");
        }
    }
}
