using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EraasoftAcademy.Migrations
{
    /// <inheritdoc />
    public partial class addCourseToQuiz_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstructorId",
                table: "Quizzes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstructorId",
                table: "Quizzes");
        }
    }
}
