using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseRegistrationSystem.Migrations
{
    public partial class RenameCoursesProperty1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CourseID",
                table: "courses",
                newName: "courseID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "courseID",
                table: "courses",
                newName: "CourseID");
        }
    }
}
