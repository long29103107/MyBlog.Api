using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBlog.Shared.Databases.Identity.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRolePermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.AddColumn<int>(
                name: "PermissionId1",
                table: "AccessRules",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoleId1",
                table: "AccessRules",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccessRules_PermissionId1",
                table: "AccessRules",
                column: "PermissionId1");

            migrationBuilder.CreateIndex(
                name: "IX_AccessRules_RoleId1",
                table: "AccessRules",
                column: "RoleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessRules_Permissions_PermissionId1",
                table: "AccessRules",
                column: "PermissionId1",
                principalTable: "Permissions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessRules_Roles_RoleId1",
                table: "AccessRules",
                column: "RoleId1",
                principalTable: "Roles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessRules_Permissions_PermissionId1",
                table: "AccessRules");

            migrationBuilder.DropForeignKey(
                name: "FK_AccessRules_Roles_RoleId1",
                table: "AccessRules");

            migrationBuilder.DropIndex(
                name: "IX_AccessRules_PermissionId1",
                table: "AccessRules");

            migrationBuilder.DropIndex(
                name: "IX_AccessRules_RoleId1",
                table: "AccessRules");

            migrationBuilder.DropColumn(
                name: "PermissionId1",
                table: "AccessRules");

            migrationBuilder.DropColumn(
                name: "RoleId1",
                table: "AccessRules");

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId_PermissionId",
                table: "RolePermissions",
                columns: new[] { "RoleId", "PermissionId" },
                unique: true);
        }
    }
}
