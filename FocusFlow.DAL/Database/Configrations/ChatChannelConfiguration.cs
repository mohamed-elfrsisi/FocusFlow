
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusFlow.DAL.Database.Configurations;

public class ChatChannelConfiguration : IEntityTypeConfiguration<ChatChannel>
{
    public void Configure(EntityTypeBuilder<ChatChannel> builder)
    {
        builder.ToTable("ChatChannels");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name).HasMaxLength(100).IsRequired();

        builder.Property(c => c.Description).HasMaxLength(500);

        builder.Property(c => c.Type).HasConversion<int>().IsRequired();

        builder.Property(c => c.IsDefault).HasDefaultValue(false);

        builder.HasIndex(c => new
            {
                c.TeamId,
                c.Name
            }).IsUnique().HasFilter("[DeletedAt] IS NULL");

        builder.HasIndex(c => c.TeamId);

        builder.HasIndex(c => c.DeletedAt);

        builder.HasIndex(c => c.TeamId).IsUnique().HasFilter("[IsDefault] = 1 AND [DeletedAt] IS NULL");
    }
}