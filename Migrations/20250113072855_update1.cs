using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseRegistrationSystem.Migrations
{
    public partial class update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "courses",
                newName: "CourseID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CourseID",
                table: "courses",
                newName: "id");
        }
    }
}
