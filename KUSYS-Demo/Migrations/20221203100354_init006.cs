using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KUSYSDemo.Migrations
{
    /// <inheritdoc />
    public partial class init006 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Student",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "EnrollmentId",
                table: "Enrollment",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CourseName",
                table: "Course",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "CourseCode",
                table: "Course",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Course",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Student",
                newName: "StudentId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Enrollment",
                newName: "EnrollmentId");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Course",
                newName: "CourseName");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Course",
                newName: "CourseCode");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Course",
                newName: "CourseId");
        }
    }
}
