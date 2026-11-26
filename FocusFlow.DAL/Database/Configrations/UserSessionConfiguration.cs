using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusFlow.DAL.Database.Configurations;

public class UserSessionConfiguration : IEntityTypeConfiguration<UserSession>
{
    public void Configure(EntityTypeBuilder<UserSession> builder)
    {
        builder.ToTable("UserSessions");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.UserId).HasMaxLength(36).IsRequired();

        builder.Property(s => s.RefreshHash).HasMaxLength(128).IsRequired();

        builder.Property(s => s.Device).HasMaxLength(256);

        builder.Property(s => s.CreatedAt).IsRequired();

        builder.Property(s => s.UpdatedAt).IsRequired();

        builder.Property(s => s.LastSeenAt).IsRequired();

        builder.HasIndex(s => s.RefreshHash).IsUnique();

        builder.HasIndex(s => s.UserId);

        builder.HasIndex(s => new
        {
            s.UserId,
            s.RevokedAt
        });

        builder.HasOne(s => s.User).WithMany(u => u.Sessions).HasForeignKey(s => s.UserId).OnDelete(DeleteBehavior.Restrict);
    }
}