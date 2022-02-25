using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetBase.Persistence.SqlServer.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Identity.Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity.Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Identity.Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    NationalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    UserName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity.Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Identity.RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ClaimValue = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity.RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Identity.RoleClaims_Identity.Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Identity.Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Identity.UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ClaimValue = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity.UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Identity.UserClaims_Identity.Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Identity.Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Identity.UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserAgent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity.UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_Identity.UserLogins_Identity.Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Identity.Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Identity.UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity.UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_Identity.UserRoles_Identity.Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Identity.Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Identity.UserRoles_Identity.Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Identity.Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Identity.UserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity.UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_Identity.UserTokens_Identity.Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Identity.Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Identity.UserUsedPasswords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HashedPassword = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity.UserUsedPasswords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Identity.UserUsedPasswords_Identity.Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Identity.Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Identity.Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName", "Title" },
                values: new object[] { 1, "ac031876-0754-487b-aaa1-25b7db68f534", "", "Admin", "ADMIN", "مدیر سیستم" });

            migrationBuilder.InsertData(
                table: "Identity.Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NationalCode", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "Status" },
                values: new object[] { 1, 0, "13b4dd94-13f2-4c0f-bb06-1ef83cd277fa", "info@datiss.net", true, "Admin", "Datiss", false, null, "0534854311", "INFO@DATISS.NET", "ADMIN", "AQAAAAEAACcQAAAAEE2rXWk1YPS4BT+RXX+4r5nNTTutPfAsJfrLBF3YVZElIrrg00ISw3Td7ODzNJh8WA==", "989120781451", true, "4a8bbbe7-a065-4f08-bb70-bb612c30571a", false, "Admin", 1 });

            migrationBuilder.InsertData(
                table: "Identity.UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Identity.RoleClaims_RoleId",
                table: "Identity.RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Identity.Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Identity.UserClaims_UserId",
                table: "Identity.UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Identity.UserLogins_UserId",
                table: "Identity.UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Identity.UserRoles_RoleId",
                table: "Identity.UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Identity.Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Identity.Users",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Identity.UserUsedPasswords_UserId",
                table: "Identity.UserUsedPasswords",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Identity.RoleClaims");

            migrationBuilder.DropTable(
                name: "Identity.UserClaims");

            migrationBuilder.DropTable(
                name: "Identity.UserLogins");

            migrationBuilder.DropTable(
                name: "Identity.UserRoles");

            migrationBuilder.DropTable(
                name: "Identity.UserTokens");

            migrationBuilder.DropTable(
                name: "Identity.UserUsedPasswords");

            migrationBuilder.DropTable(
                name: "Identity.Roles");

            migrationBuilder.DropTable(
                name: "Identity.Users");
        }
    }
}
