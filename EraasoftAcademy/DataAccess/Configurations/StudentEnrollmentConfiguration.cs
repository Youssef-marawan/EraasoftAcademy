using EraasoftAcademy.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EraasoftAcademy.DataAccess.Configurations
{
    public class StudentEnrollmentConfiguration : IEntityTypeConfiguration<StudentEnrollment>
    {
        public void Configure(EntityTypeBuilder<StudentEnrollment> builder)
        {
            builder.HasKey(e => e.EnrollmentId);

            builder.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.EnrollmentDate)
                .IsRequired();
/*
            builder.HasOne(e => e.Student)
                .WithMany(s => s.StudentEnrollments)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Course)
                .WithMany(c => c.StudentEnrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
*/
        }
    }
}
