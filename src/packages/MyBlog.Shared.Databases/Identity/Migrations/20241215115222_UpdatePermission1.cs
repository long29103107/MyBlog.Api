using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBlog.Shared.Databases.Identity.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePermission1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessRules_Permissions_PermissionId",
                table: "AccessRules");

            migrationBuilder.DropForeignKey(
                name: "FK_AccessRules_Roles_RoleId",
                table: "AccessRules");

            migrationBuilder.DropForeignKey(
                name: "FK_AccessRules_Roles_RoleId1",
                table: "AccessRules");

            migrationBuilder.DropIndex(
                name: "IX_AccessRules_RoleId1",
                table: "AccessRules");

            migrationBuilder.DropColumn(
                name: "RoleId1",
                table: "AccessRules");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessRules_Permissions_PermissionId",
                table: "AccessRules",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccessRules_Roles_RoleId",
                table: "AccessRules",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessRules_Permissions_PermissionId",
                table: "AccessRules");

            migrationBuilder.DropForeignKey(
                name: "FK_AccessRules_Roles_RoleId",
                table: "AccessRules");

            migrationBuilder.AddColumn<int>(
                name: "RoleId1",
                table: "AccessRules",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccessRules_RoleId1",
                table: "AccessRules",
                column: "RoleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessRules_Permissions_PermissionId",
                table: "AccessRules",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccessRules_Roles_RoleId",
                table: "AccessRules",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccessRules_Roles_RoleId1",
                table: "AccessRules",
                column: "RoleId1",
                principalTable: "Roles",
                principalColumn: "Id");
        }
    }
}
