using EraasoftAcademy.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TeacherConfig : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.HasKey(x => x.Teacher_Id);

        builder.HasOne(x => x.User)
            .WithOne(x => x.Teacher)
            .HasForeignKey<Teacher>(x => x.User_Id);

        builder.Property(x => x.Specialization)
            .HasMaxLength(100);
    }
}