
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusFlow.DAL.Database.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id).HasMaxLength(36).IsRequired();

        builder.Property(u => u.Email).HasMaxLength(256).IsRequired();

        builder.Property(u => u.Nickname).HasMaxLength(50).IsRequired();

        builder.Property(u => u.DisplayName).HasMaxLength(100).IsRequired();

        builder.Property(u => u.PasswordHash).HasMaxLength(500).IsRequired();

        builder.Property(u => u.AboutMe).HasMaxLength(1000);

        builder.Property(u => u.Timezone).HasMaxLength(100).IsRequired();

        builder.HasIndex(u => u.Email).IsUnique();

        builder.HasIndex(u => u.Status);

        builder.HasIndex(u => u.DeletedAt);

        builder.HasIndex(u => u.ScheduledDeletionAt);

        builder.Property(u => u.WeekStart).HasDefaultValue((byte)1);

        builder.Property(u => u.EmailVerified).HasDefaultValue(false);

        builder.Property(u => u.Role).HasConversion<int>();

        builder.Property(u => u.Status).HasConversion<int>();

        builder.Property(u => u.ProfileVisibility).HasConversion<int>();
    }
}