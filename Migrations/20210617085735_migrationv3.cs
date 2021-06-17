using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class migrationv3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "title",
                table: "Activities",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Activities",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "category",
                table: "Activities",
                newName: "Category");

            migrationBuilder.RenameColumn(
                name: "time",
                table: "Activities",
                newName: "Date");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Activities",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Venue",
                table: "Activities",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Venue",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Activities",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Activities",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Activities",
                newName: "category");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Activities",
                newName: "time");
        }
    }
}
