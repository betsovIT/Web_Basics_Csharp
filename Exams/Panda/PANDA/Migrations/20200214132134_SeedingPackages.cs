using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PandaWebApp.Migrations
{
    public partial class SeedingPackages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Packages",
                columns: new[] { "Id", "Description", "EstimatedDeliveryDate", "RecipientId", "ShippingAddress", "Status", "Weight" },
                values: new object[] { "da9be334-bccc-496d-bfc1-b130dff0c7cf", "abcdefgh", new DateTime(2020, 3, 15, 13, 21, 34, 183, DateTimeKind.Utc).AddTicks(4766), "0e69d161-4fe0-430d-b2aa-3347a06fce88", "Bor N2", 0, 2.0 });

            migrationBuilder.InsertData(
                table: "Packages",
                columns: new[] { "Id", "Description", "EstimatedDeliveryDate", "RecipientId", "ShippingAddress", "Status", "Weight" },
                values: new object[] { "f07cf75a-7106-4f4a-b4a8-84c8b38bca94", "12121212", new DateTime(2020, 3, 15, 13, 21, 34, 184, DateTimeKind.Utc).AddTicks(8839), "0e69d161-4fe0-430d-b2aa-3347a06fce88", "Bor N2", 1, 2.0 });

            migrationBuilder.InsertData(
                table: "Packages",
                columns: new[] { "Id", "Description", "EstimatedDeliveryDate", "RecipientId", "ShippingAddress", "Status", "Weight" },
                values: new object[] { "5f28c359-875f-4a58-be74-47e628ef24df", "a34a123", new DateTime(2020, 3, 15, 13, 21, 34, 184, DateTimeKind.Utc).AddTicks(8912), "0e69d161-4fe0-430d-b2aa-3347a06fce88", "Bor N2", 2, 2.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: "5f28c359-875f-4a58-be74-47e628ef24df");

            migrationBuilder.DeleteData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: "da9be334-bccc-496d-bfc1-b130dff0c7cf");

            migrationBuilder.DeleteData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: "f07cf75a-7106-4f4a-b4a8-84c8b38bca94");
        }
    }
}
