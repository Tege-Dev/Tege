using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoteTakingApp.Migrations
{
    /// <inheritdoc />
    public partial class Update_structure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Notes");

            migrationBuilder.RenameColumn(
                name: "Theme",
                table: "Notes",
                newName: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Notes",
                newName: "Theme");

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
