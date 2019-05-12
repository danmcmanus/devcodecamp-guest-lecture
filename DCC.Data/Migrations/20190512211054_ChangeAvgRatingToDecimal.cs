using Microsoft.EntityFrameworkCore.Migrations;

namespace DCC.Data.Migrations
{
    public partial class ChangeAvgRatingToDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "AverageRating",
                table: "Instructors",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "AverageRating",
                table: "Instructors",
                nullable: true,
                oldClrType: typeof(decimal));
        }
    }
}
