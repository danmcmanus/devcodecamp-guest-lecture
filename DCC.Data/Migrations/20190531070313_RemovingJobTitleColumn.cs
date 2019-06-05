using Microsoft.EntityFrameworkCore.Migrations;

namespace DCC.Data.Migrations
{
    public partial class RemovingJobTitleColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobTitle",
                table: "Instructors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "Instructors",
                nullable: true);
        }
    }
}
