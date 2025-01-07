using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncSpace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addforgetpassworddattouser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ForgetPasswordResetCodeRequestedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordResetTokenRequestedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForgetPasswordResetCodeRequestedAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PasswordResetTokenRequestedAt",
                table: "AspNetUsers");
        }
    }
}
