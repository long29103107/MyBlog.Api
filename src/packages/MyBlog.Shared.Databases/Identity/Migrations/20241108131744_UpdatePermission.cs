using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBlog.Shared.Databases.Identity.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operations_Permissions_PermissionId",
                table: "Operations");

            migrationBuilder.DropIndex(
                name: "IX_Operations_PermissionId",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "PermissionId",
                table: "Operations");

            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "Permissions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "Operations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "OperationPermission",
                columns: table => new
                {
                    OperationsId = table.Column<int>(type: "int", nullable: false),
                    PermissionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationPermission", x => new { x.OperationsId, x.PermissionsId });
                    table.ForeignKey(
                        name: "FK_OperationPermission_Operations_OperationsId",
                        column: x => x.OperationsId,
                        principalTable: "Operations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OperationPermission_Permissions_PermissionsId",
                        column: x => x.PermissionsId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OperationPermission_PermissionsId",
                table: "OperationPermission",
                column: "PermissionsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationPermission");

            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "Operations");

            migrationBuilder.AddColumn<int>(
                name: "PermissionId",
                table: "Operations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Operations_PermissionId",
                table: "Operations",
                column: "PermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_Permissions_PermissionId",
                table: "Operations",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id");
        }
    }
}
