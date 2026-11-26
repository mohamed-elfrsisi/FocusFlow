using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusFlow.DAL.Database.Configurations;

public class AuthTokenConfiguration : IEntityTypeConfiguration<AuthToken>
{
    public void Configure(EntityTypeBuilder<AuthToken> builder)
    {
        builder.ToTable("AuthTokens");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.UserId).HasMaxLength(36).IsRequired();

        builder.Property(t => t.TokenHash).HasMaxLength(128).IsRequired();

        builder.Property(t => t.Type).HasConversion<int>().IsRequired();

        builder.Property(t => t.ExpiresAt).IsRequired();

        builder.Property(t => t.CreatedAt).IsRequired();

        builder.HasIndex(t => t.TokenHash).IsUnique();

        builder.HasIndex(t => new
        {
            t.UserId,
            t.Type
        });

        builder.HasIndex(t => t.ExpiresAt);

        builder.HasOne(t => t.User).WithMany(u => u.AuthTokens).HasForeignKey(t => t.UserId).OnDelete(DeleteBehavior.Restrict);
    }
}