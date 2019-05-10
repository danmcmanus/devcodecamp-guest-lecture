using Microsoft.EntityFrameworkCore.Migrations;

namespace DCC.Data.Migrations
{
    public partial class RemoveBioField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Position",
                table: "Instructors",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Instructors",
                newName: "JobTitle");

            migrationBuilder.RenameColumn(
                name: "InstructorBio",
                table: "Instructors",
                newName: "FirstName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Instructors",
                newName: "Position");

            migrationBuilder.RenameColumn(
                name: "JobTitle",
                table: "Instructors",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Instructors",
                newName: "InstructorBio");
        }
    }
}
