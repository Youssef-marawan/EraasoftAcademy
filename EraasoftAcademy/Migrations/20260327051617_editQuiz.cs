using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EraasoftAcademy.Migrations
{
    /// <inheritdoc />
    public partial class editQuiz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Quizzes_QuizCode",
                table: "Quizzes");

            migrationBuilder.AlterColumn<string>(
                name: "QuizCode",
                table: "Quizzes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_QuizCode",
                table: "Quizzes",
                column: "QuizCode",
                unique: true,
                filter: "[QuizCode] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Quizzes_QuizCode",
                table: "Quizzes");

            migrationBuilder.AlterColumn<string>(
                name: "QuizCode",
                table: "Quizzes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_QuizCode",
                table: "Quizzes",
                column: "QuizCode",
                unique: true);
        }
    }
}
