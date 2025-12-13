using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityService.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddISVerifiedinUsertoken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsUsed",
                table: "UserTokens",
                newName: "IsVerified");

            migrationBuilder.AddColumn<DateTime>(
                name: "VerifiedAt",
                table: "UserTokens",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerifiedAt",
                table: "UserTokens");

            migrationBuilder.RenameColumn(
                name: "IsVerified",
                table: "UserTokens",
                newName: "IsUsed");
        }
    }
}
