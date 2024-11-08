using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBlog.Shared.Databases.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddOperationPermissionRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationPermission_Operations_OperationsId",
                table: "OperationPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationPermission_Permissions_PermissionsId",
                table: "OperationPermission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OperationPermission",
                table: "OperationPermission");

            migrationBuilder.DropIndex(
                name: "IX_OperationPermission_PermissionsId",
                table: "OperationPermission");

            migrationBuilder.RenameColumn(
                name: "PermissionsId",
                table: "OperationPermission",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "OperationsId",
                table: "OperationPermission",
                newName: "PermissionId");

            migrationBuilder.AddColumn<int>(
                name: "OperationId",
                table: "OperationPermission",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OperationPermission",
                table: "OperationPermission",
                columns: new[] { "OperationId", "PermissionId" });

            migrationBuilder.CreateIndex(
                name: "IX_OperationPermission_PermissionId",
                table: "OperationPermission",
                column: "PermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationPermission_Operations_OperationId",
                table: "OperationPermission",
                column: "OperationId",
                principalTable: "Operations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationPermission_Permissions_PermissionId",
                table: "OperationPermission",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationPermission_Operations_OperationId",
                table: "OperationPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationPermission_Permissions_PermissionId",
                table: "OperationPermission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OperationPermission",
                table: "OperationPermission");

            migrationBuilder.DropIndex(
                name: "IX_OperationPermission_PermissionId",
                table: "OperationPermission");

            migrationBuilder.DropColumn(
                name: "OperationId",
                table: "OperationPermission");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "OperationPermission",
                newName: "PermissionsId");

            migrationBuilder.RenameColumn(
                name: "PermissionId",
                table: "OperationPermission",
                newName: "OperationsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OperationPermission",
                table: "OperationPermission",
                columns: new[] { "OperationsId", "PermissionsId" });

            migrationBuilder.CreateIndex(
                name: "IX_OperationPermission_PermissionsId",
                table: "OperationPermission",
                column: "PermissionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationPermission_Operations_OperationsId",
                table: "OperationPermission",
                column: "OperationsId",
                principalTable: "Operations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationPermission_Permissions_PermissionsId",
                table: "OperationPermission",
                column: "PermissionsId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
