using DiscountService.Domain.DomainModel.DiscountDomainModel;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace DiscountService.Persistence.Migrations
{
    public partial class SampleData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Coupon",
                columns: new[] { "Id", "UserId", "DiscountAmount", "Version" },
                values: new object[,]
                {
                    { "coupon-9f17a40b-4322-4eeb-be6d-2e30b1b93542", "fec555e7-e498-43d9-a977-afb8c867c523", 67, 1},
                    { "coupon-b9ae220b-a507-4a8d-b105-690d1c3b18a4", "094da2e7-c73d-4d13-8191-3832c02ec105", 96, 1}
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coupon",
                keyColumn: "Id",
                keyValues: new object[]
                {
                   "coupon-9f17a40b-4322-4eeb-be6d-2e30b1b93542",
                   "coupon-b9ae220b-a507-4a8d-b105-690d1c3b18a4"
                });
        }
    }
}
