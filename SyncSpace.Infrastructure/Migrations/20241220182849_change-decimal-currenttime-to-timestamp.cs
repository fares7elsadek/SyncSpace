using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncSpace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changedecimalcurrenttimetotimestamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentTime",
                table: "Rooms");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "CurrentVideoTime",
                table: "Rooms",
                type: "time",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentVideoTime",
                table: "Rooms");

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentTime",
                table: "Rooms",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true);
        }
    }
}
