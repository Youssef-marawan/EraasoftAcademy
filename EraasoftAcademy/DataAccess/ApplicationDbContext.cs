using EraasoftAcademy.Models;
using Microsoft.EntityFrameworkCore;

namespace EraasoftAcademy.DataAccess
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<QuestionChoices> QuestionChoices { get; set; }
        public DbSet<QuizAttempt> QuizAttempts { get; set; }
        public DbSet<StudentAnswer> StudentAnswers { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<StudentAttendance> StudentAttendances { get; set; }
        public DbSet<StudentEnrollment> StudentEnrollments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Apply all configurations from the current assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

    }
}
