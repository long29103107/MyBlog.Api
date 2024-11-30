using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBlog.Shared.Databases.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddScope : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessRules_Operations_OperationId",
                table: "AccessRules");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Roles_RoleId",
                table: "Permissions");

            migrationBuilder.DropTable(
                name: "OperationPermissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_Code",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_RoleId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_AccessRules_OperationId",
                table: "AccessRules");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "OperationId",
                table: "AccessRules");

            migrationBuilder.AddColumn<int>(
                name: "OperationId",
                table: "Permissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ScopeId",
                table: "Permissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Scopes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scopes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_OperationId",
                table: "Permissions",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ScopeId_OperationId",
                table: "Permissions",
                columns: new[] { "ScopeId", "OperationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Scopes_Code",
                table: "Scopes",
                column: "Code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Operations_OperationId",
                table: "Permissions",
                column: "OperationId",
                principalTable: "Operations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Scopes_ScopeId",
                table: "Permissions",
                column: "ScopeId",
                principalTable: "Scopes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Operations_OperationId",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Scopes_ScopeId",
                table: "Permissions");

            migrationBuilder.DropTable(
                name: "Scopes");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_OperationId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_ScopeId_OperationId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "OperationId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "ScopeId",
                table: "Permissions");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Permissions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "Permissions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Permissions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Permissions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Operations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Operations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "Operations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Operations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Operations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OperationId",
                table: "AccessRules",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OperationPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperationId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationPermissions_Operations_OperationId",
                        column: x => x.OperationId,
                        principalTable: "Operations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OperationPermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Code",
                table: "Permissions",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_RoleId",
                table: "Permissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessRules_OperationId",
                table: "AccessRules",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationPermissions_OperationId_PermissionId",
                table: "OperationPermissions",
                columns: new[] { "OperationId", "PermissionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OperationPermissions_PermissionId",
                table: "OperationPermissions",
                column: "PermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessRules_Operations_OperationId",
                table: "AccessRules",
                column: "OperationId",
                principalTable: "Operations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Roles_RoleId",
                table: "Permissions",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id");
        }
    }
}
