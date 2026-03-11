using EraasoftAcademy.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EraasoftAcademy.DataAccess.Configurations
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.HasKey(s => s.SessionId);

            builder.Property(s => s.RoomNUM)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(s => s.SessionDate)
                .IsRequired();

            //builder.HasOne(s => s.Course)
            //    .WithMany(c => c.Sessions)
            //    .HasForeignKey(s => s.CourseId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
