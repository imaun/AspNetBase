using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetBase.Persistence.SqlServer.Migrations
{
    public partial class AppClaimType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Identity.AppClaimTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity.AppClaimTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Identity.AppClaimTypes",
                columns: new[] { "Id", "Description", "Name", "Title" },
                values: new object[,]
                {
                    { 1, "مدیریت کاربران", "Users", "کاربران" },
                    { 2, "مدیریت نقش ها و دسترسی های کاربران", "Roles", "نقش های کاربری" }
                });

            migrationBuilder.UpdateData(
                table: "Identity.Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "dd5f00b4-9b09-48a7-b51e-7d777951361d");

            migrationBuilder.UpdateData(
                table: "Identity.Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4a463320-c615-476a-b951-5bc63ddde5a2", "AQAAAAEAACcQAAAAEBLAbk8tidGUhEwIJxo79WIgC235FQl5lrvSXQmn5PrDP4Fim2EZPpC8nuu87UOcxA==", "fcfadd30-0c49-490a-844b-1ed0f6755cca" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Identity.AppClaimTypes");

            migrationBuilder.UpdateData(
                table: "Identity.Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "11103bde-9122-467f-8f81-84809b9a374a");

            migrationBuilder.UpdateData(
                table: "Identity.Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "75632978-a161-414d-bfc4-c7c7a487df73", "AQAAAAEAACcQAAAAEPgbJ9CeBWqU3MazpIWPJONPknmubGJQ1M2gSUwUqwuqkwBjHnHV6KWg2F8Cm4K5Wg==", "5d42424c-2f81-49ba-9c32-7e17d82f97fd" });
        }
    }
}
