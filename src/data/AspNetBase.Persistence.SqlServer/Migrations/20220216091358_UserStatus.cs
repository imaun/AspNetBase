using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetBase.Persistence.SqlServer.Migrations
{
    public partial class UserStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Identity.Users",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.UpdateData(
                table: "Identity.Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "f7b5a209-0695-4dd0-8b9a-0852ab588f3c");

            migrationBuilder.UpdateData(
                table: "Identity.Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Status" },
                values: new object[] { "e76da048-c166-4a1d-8ef0-7509aab92d29", "AQAAAAEAACcQAAAAEJLcg50g/oJAMiilwAPyfozsZ3tb+fO/u45Ms+3nTqHrALiMC0mzeGnbuyhPNw5lbQ==", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Identity.Users");

            migrationBuilder.UpdateData(
                table: "Identity.Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "1936f0b0-6a81-4d8d-8837-b4df052f74f2");

            migrationBuilder.UpdateData(
                table: "Identity.Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5c48075f-3586-408f-a55f-f7471bb1baa0", "AQAAAAEAACcQAAAAEMIpaPz/u3lL0krEB2NM6paoVYYn2WULj2Nwj//xzPst+e02BFjRqgge8j8aNggY3Q==" });
        }
    }
}
