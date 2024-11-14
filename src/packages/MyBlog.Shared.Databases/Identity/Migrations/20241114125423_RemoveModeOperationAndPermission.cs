using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBlog.Shared.Databases.Identity.Migrations
{
    /// <inheritdoc />
    public partial class RemoveModeOperationAndPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mode",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "Mode",
                table: "Operations");

            migrationBuilder.AddColumn<bool>(
                name: "Mode",
                table: "AccessRules",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mode",
                table: "AccessRules");

            migrationBuilder.AddColumn<bool>(
                name: "Mode",
                table: "Permissions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Mode",
                table: "Operations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
