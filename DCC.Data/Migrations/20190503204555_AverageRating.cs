using Microsoft.EntityFrameworkCore.Migrations;

namespace DCC.Data.Migrations
{
    public partial class AverageRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Instructors");

            migrationBuilder.AddColumn<int>(
                name: "AggregateRatings",
                table: "Instructors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "AverageRating",
                table: "Instructors",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfRatings",
                table: "Instructors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Instructors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AggregateRatings",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "AverageRating",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "NumberOfRatings",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Instructors");

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Instructors",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
