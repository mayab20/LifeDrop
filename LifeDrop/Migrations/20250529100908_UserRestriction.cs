using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LifeDrop.Migrations
{
    /// <inheritdoc />
    public partial class UserRestriction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastDonationDate",
                table: "user",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<bool>(
                name: "IsRestricted",
                table: "user",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRestricted",
                table: "user");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "LastDonationDate",
                table: "user",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);
        }
    }
}
