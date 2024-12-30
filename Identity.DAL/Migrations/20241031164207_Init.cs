using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "permission",
                columns: table => new
                {
                    Tênquyền = table.Column<string>(name: "Tên quyền", type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Môtả = table.Column<string>(name: "Mô tả", type: "ntext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permission", x => x.Tênquyền);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    Tênvaitrò = table.Column<string>(name: "Tên vai trò", type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Môtả = table.Column<string>(name: "Mô tả", type: "ntext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.Tênvaitrò);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Ngàysinh = table.Column<DateTime>(name: "Ngày sinh", type: "datetime2", nullable: false),
                    Ngàytạo = table.Column<DateTime>(name: "Ngày tạo", type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "role-permission",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    PermissionId = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role-permission", x => new { x.PermissionId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_role-permission_permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "permission",
                        principalColumn: "Tên quyền",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role-permission_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "role",
                        principalColumn: "Tên vai trò",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user-role",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user-role", x => new { x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_user-role_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "role",
                        principalColumn: "Tên vai trò",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user-role_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_role-permission_RoleId",
                table: "role-permission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_user_Username",
                table: "user",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user-role_UserId",
                table: "user-role",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "role-permission");

            migrationBuilder.DropTable(
                name: "user-role");

            migrationBuilder.DropTable(
                name: "permission");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
