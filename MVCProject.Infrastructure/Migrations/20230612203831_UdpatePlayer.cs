using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UdpatePlayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GamePlayed",
                table: "Players",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GameWon",
                table: "Players",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GamePlayed",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "GameWon",
                table: "Players");
        }
    }
}
