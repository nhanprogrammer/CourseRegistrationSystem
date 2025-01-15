using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseRegistrationSystem.Migrations
{
    public partial class RenameCoursesProperty2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "courseID",
                table: "courses",
                newName: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "courses",
                newName: "courseID");
        }
    }
}
