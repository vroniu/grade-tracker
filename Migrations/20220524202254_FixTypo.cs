using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace grade_tracker.Migrations
{
    /// <inheritdoc />
    public partial class FixTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTeacher_Teachers_TeachersStudentId",
                table: "SubjectTeacher");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Teachers",
                newName: "TeacherId");

            migrationBuilder.RenameColumn(
                name: "TeachersStudentId",
                table: "SubjectTeacher",
                newName: "TeachersTeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectTeacher_TeachersStudentId",
                table: "SubjectTeacher",
                newName: "IX_SubjectTeacher_TeachersTeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTeacher_Teachers_TeachersTeacherId",
                table: "SubjectTeacher",
                column: "TeachersTeacherId",
                principalTable: "Teachers",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTeacher_Teachers_TeachersTeacherId",
                table: "SubjectTeacher");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "Teachers",
                newName: "StudentId");

            migrationBuilder.RenameColumn(
                name: "TeachersTeacherId",
                table: "SubjectTeacher",
                newName: "TeachersStudentId");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectTeacher_TeachersTeacherId",
                table: "SubjectTeacher",
                newName: "IX_SubjectTeacher_TeachersStudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTeacher_Teachers_TeachersStudentId",
                table: "SubjectTeacher",
                column: "TeachersStudentId",
                principalTable: "Teachers",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
