﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Games");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Games",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
