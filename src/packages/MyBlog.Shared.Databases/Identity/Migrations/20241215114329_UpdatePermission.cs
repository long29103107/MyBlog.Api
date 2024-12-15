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
                name: "FK_AccessRules_Permissions_PermissionId",
                table: "AccessRules");

            migrationBuilder.DropForeignKey(
                name: "FK_AccessRules_Permissions_PermissionId1",
                table: "AccessRules");

            migrationBuilder.DropForeignKey(
                name: "FK_AccessRules_Roles_RoleId",
                table: "AccessRules");

            migrationBuilder.DropIndex(
                name: "IX_AccessRules_PermissionId1",
                table: "AccessRules");

            migrationBuilder.DropColumn(
                name: "PermissionId1",
                table: "AccessRules");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Weight",
                table: "Roles",
                column: "Weight",
                unique: true);

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

            migrationBuilder.DropIndex(
                name: "IX_Roles_Weight",
                table: "Roles");

            migrationBuilder.AddColumn<int>(
                name: "PermissionId1",
                table: "AccessRules",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccessRules_PermissionId1",
                table: "AccessRules",
                column: "PermissionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessRules_Permissions_PermissionId",
                table: "AccessRules",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccessRules_Permissions_PermissionId1",
                table: "AccessRules",
                column: "PermissionId1",
                principalTable: "Permissions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessRules_Roles_RoleId",
                table: "AccessRules",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
