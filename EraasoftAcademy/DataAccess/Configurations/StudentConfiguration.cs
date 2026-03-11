public class StudentConfig : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(x => x.Student_Id);

        builder.HasOne(x => x.User)
            .WithOne(x => x.Student)
            .HasForeignKey<Student>(x => x.User_Id);

        builder.Property(x => x.EnrollmentDate)
            .IsRequired();
    }
}