using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PortFreight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "Name", "Phone" },
                values: new object[] { new Guid("6d283de6-b929-45ce-bec4-087ada6aa777"), "info@acme.com", "Acme Logistics", "+44 1234 567890" });

            migrationBuilder.InsertData(
                table: "Ports",
                columns: new[] { "Id", "Country", "Name", "UNLocode" },
                values: new object[,]
                {
                    { new Guid("0bb7bbe2-3bdb-4298-82f0-b921a79c1af5"), "Netherlands", "Port of Rotterdam", "NLRTM" },
                    { new Guid("5e423b0c-879d-4612-a690-e9bf5de4ce41"), "United Kingdom", "Port of Belfast", "GBBEL" }
                });

            migrationBuilder.InsertData(
                table: "Vessels",
                columns: new[] { "Id", "CapacityTEU", "IMO", "Name" },
                values: new object[] { new Guid("28de9fe0-cfbb-4dc1-bb44-23497b16c292"), 12000, "9876543", "MV Horizon" });

            migrationBuilder.InsertData(
                table: "VesselVoyages",
                columns: new[] { "Id", "ArrivalPortId", "ArrivalTime", "DeparturePortId", "DepartureTime", "VesselId", "VoyageNumber" },
                values: new object[] { new Guid("f905bcbb-1101-46a9-a503-aa01212c72fb"), new Guid("0bb7bbe2-3bdb-4298-82f0-b921a79c1af5"), new DateTime(2026, 2, 10, 13, 0, 0, 0, DateTimeKind.Unspecified), new Guid("5e423b0c-879d-4612-a690-e9bf5de4ce41"), new DateTime(2026, 2, 11, 13, 0, 0, 0, DateTimeKind.Unspecified), new Guid("28de9fe0-cfbb-4dc1-bb44-23497b16c292"), "HZ123" });

            migrationBuilder.InsertData(
                table: "Shipments",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DestinationPortId", "OriginPortId", "ReferenceNumber", "Status", "VesselVoyageId" },
                values: new object[] { new Guid("e14cf002-1fae-4a03-9d9a-75f20f9631a1"), new DateTime(2026, 2, 12, 13, 0, 0, 0, DateTimeKind.Unspecified), new Guid("6d283de6-b929-45ce-bec4-087ada6aa777"), new Guid("0bb7bbe2-3bdb-4298-82f0-b921a79c1af5"), new Guid("5e423b0c-879d-4612-a690-e9bf5de4ce41"), "SHIP-0001", 1, new Guid("f905bcbb-1101-46a9-a503-aa01212c72fb") });

            migrationBuilder.InsertData(
                table: "Containers",
                columns: new[] { "Id", "ContainerNumber", "ShipmentId", "Type", "Weight" },
                values: new object[] { new Guid("09f83d1a-0b7b-42d5-b830-f6051be5db7e"), "CONT1234567", new Guid("e14cf002-1fae-4a03-9d9a-75f20f9631a1"), "40ft", 12000m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Containers",
                keyColumn: "Id",
                keyValue: new Guid("09f83d1a-0b7b-42d5-b830-f6051be5db7e"));

            migrationBuilder.DeleteData(
                table: "Shipments",
                keyColumn: "Id",
                keyValue: new Guid("e14cf002-1fae-4a03-9d9a-75f20f9631a1"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("6d283de6-b929-45ce-bec4-087ada6aa777"));

            migrationBuilder.DeleteData(
                table: "VesselVoyages",
                keyColumn: "Id",
                keyValue: new Guid("f905bcbb-1101-46a9-a503-aa01212c72fb"));

            migrationBuilder.DeleteData(
                table: "Ports",
                keyColumn: "Id",
                keyValue: new Guid("0bb7bbe2-3bdb-4298-82f0-b921a79c1af5"));

            migrationBuilder.DeleteData(
                table: "Ports",
                keyColumn: "Id",
                keyValue: new Guid("5e423b0c-879d-4612-a690-e9bf5de4ce41"));

            migrationBuilder.DeleteData(
                table: "Vessels",
                keyColumn: "Id",
                keyValue: new Guid("28de9fe0-cfbb-4dc1-bb44-23497b16c292"));
        }
    }
}
