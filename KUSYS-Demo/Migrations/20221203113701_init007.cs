using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KUSYSDemo.Migrations
{
    /// <inheritdoc />
    public partial class init007 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TempEnrollmentId",
                table: "Course",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TempEnrollment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempEnrollment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TempEnrollment_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Course_TempEnrollmentId",
                table: "Course",
                column: "TempEnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TempEnrollment_StudentId",
                table: "TempEnrollment",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_TempEnrollment_TempEnrollmentId",
                table: "Course",
                column: "TempEnrollmentId",
                principalTable: "TempEnrollment",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_TempEnrollment_TempEnrollmentId",
                table: "Course");

            migrationBuilder.DropTable(
                name: "TempEnrollment");

            migrationBuilder.DropIndex(
                name: "IX_Course_TempEnrollmentId",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "TempEnrollmentId",
                table: "Course");
        }
    }
}
