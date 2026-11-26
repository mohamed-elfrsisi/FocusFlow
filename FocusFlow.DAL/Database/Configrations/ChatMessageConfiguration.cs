using FocusFlow.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusFlow.DAL.Database.Configurations;

public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
{
    public void Configure(EntityTypeBuilder<ChatMessage> builder)
    {
        builder.ToTable("ChatMessages");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.ChannelId).IsRequired();

        builder.Property(m => m.SenderId).HasMaxLength(36).IsRequired();

        builder.Property(m => m.Body).HasMaxLength(4000).IsRequired();

        builder.Property(m => m.CreatedAt).IsRequired();

        builder.HasIndex(m => new
        {
            m.ChannelId,
            m.CreatedAt
        });

        builder.HasIndex(m => m.SenderId);

        builder.HasIndex(m => m.DeletedAt);

        builder.HasOne(m => m.Channel).WithMany(c => c.Messages).HasForeignKey(m => m.ChannelId).OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.Sender).WithMany(u => u.SentMessages).HasForeignKey(m => m.SenderId).OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.DeletedBy).WithMany(u => u.DeletedMessages).HasForeignKey(m => m.DeletedById).OnDelete(DeleteBehavior.SetNull);
    }
}