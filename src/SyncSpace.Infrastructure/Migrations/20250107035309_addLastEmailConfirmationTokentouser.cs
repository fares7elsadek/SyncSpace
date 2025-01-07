using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncSpace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addLastEmailConfirmationTokentouser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastEmailConfirmationToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastEmailConfirmationToken",
                table: "AspNetUsers");
        }
    }
}
