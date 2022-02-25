using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetBase.Persistence.SqlServer.Migrations
{
    public partial class AddUserLoginKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Identity.Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ac031876-0754-487b-aaa1-25b7db68f534");

            migrationBuilder.UpdateData(
                table: "Identity.Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "13b4dd94-13f2-4c0f-bb06-1ef83cd277fa", "AQAAAAEAACcQAAAAEE2rXWk1YPS4BT+RXX+4r5nNTTutPfAsJfrLBF3YVZElIrrg00ISw3Td7ODzNJh8WA==", "4a8bbbe7-a065-4f08-bb70-bb612c30571a" });
        }
    }
}
