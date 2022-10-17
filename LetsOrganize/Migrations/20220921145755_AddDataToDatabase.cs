using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LetsOrganize.Migrations
{
    public partial class AddDataToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Elements",
                columns: new[] { "Id", "MyListId", "Name", "Quantity", "Unit" },
                values: new object[] { 1, 1, "Apples", 1f, "kg" });

            migrationBuilder.InsertData(
                table: "Elements",
                columns: new[] { "Id", "MyListId", "Name", "Quantity", "Unit" },
                values: new object[] { 2, 1, "Potatoes", 3f, "kg" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Elements",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Elements",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
