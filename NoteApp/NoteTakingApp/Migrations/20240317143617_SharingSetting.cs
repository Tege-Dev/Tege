using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoteTakingApp.Migrations
{
    /// <inheritdoc />
    public partial class SharingSetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sharing",
                table: "Notes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sharing",
                table: "Notes");

        }
    }
}
