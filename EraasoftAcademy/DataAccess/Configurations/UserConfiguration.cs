using EraasoftAcademy.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.User_Id);

        builder.Property(x => x.F_Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.L_Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Role)
            .IsRequired();
    }
}