using EraasoftAcademy.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EraasoftAcademy.DataAccess.Configurations
{
    public class StudentAttendanceConfiguration : IEntityTypeConfiguration<StudentAttendance>
    {
        public void Configure(EntityTypeBuilder<StudentAttendance> builder)
        {
            builder.HasKey(a => a.AttendanceId);

            builder.Property(a => a.Status)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(a => a.Note)
                .HasMaxLength(200);

            //builder.HasOne(a => a.Session)
            //    .WithMany(s => s.StudentAttendances)
            //    .HasForeignKey(a => a.SessionId);

            builder.HasOne(a => a.Enrollment)
                .WithMany(e => e.StudentAttendances)
                .HasForeignKey(a => a.EnrollmentId);
        }
    }
}
