using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
         
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "TbSetting");

            migrationBuilder.DropTable(
                name: "TbShippmentStatus");

            migrationBuilder.DropTable(
                name: "TbUserSubscriptions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "TbCarriers");

            migrationBuilder.DropTable(
                name: "TbShippments");

            migrationBuilder.DropTable(
                name: "TbSubscriptionPackages");

            migrationBuilder.DropTable(
                name: "TbPaymentMethods");

            migrationBuilder.DropTable(
                name: "TbShippingTypes");

            migrationBuilder.DropTable(
                name: "TbUserReceivers");

            migrationBuilder.DropTable(
                name: "TbUserSebders");

            migrationBuilder.DropTable(
                name: "TbCities");

            migrationBuilder.DropTable(
                name: "TbCountries");
        }
    }
}
