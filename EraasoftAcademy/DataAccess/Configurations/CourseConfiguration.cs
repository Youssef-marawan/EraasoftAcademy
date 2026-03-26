using EraasoftAcademy.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CourseConfig : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(x => x.Course_Id);

        builder.Property(x => x.Course_Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(x => x.Teacher)
            .WithMany(x => x.Courses)
            .HasForeignKey(x => x.Teacher_Id);

        builder.HasMany(c => c.Quizzes)
                    .WithOne(q => q.Course)
                   .HasForeignKey(q => q.Id)
                   .OnDelete(DeleteBehavior.Cascade);
    }
}