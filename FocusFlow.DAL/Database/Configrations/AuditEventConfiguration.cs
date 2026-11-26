using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusFlow.DAL.Database.Configurations;

public class AuditEventConfiguration : IEntityTypeConfiguration<AuditEvent>
{
    public void Configure(EntityTypeBuilder<AuditEvent> builder)
    {
        builder.ToTable("AuditEvents");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.ActorId).HasMaxLength(36);

        builder.Property(a => a.Action).HasMaxLength(100).IsRequired();

        builder.Property(a => a.TargetType).HasMaxLength(100).IsRequired();

        builder.Property(a => a.TargetId).HasMaxLength(128);

        builder.Property(a => a.Description).HasMaxLength(2000);

        builder.Property(a => a.IpAddress).HasMaxLength(45);

        builder.Property(a => a.UserAgent).HasMaxLength(1024);

        builder.Property(a => a.Outcome).HasConversion<int>().IsRequired();

        builder.Property(a => a.CreatedAt).IsRequired();

        builder.HasIndex(a => a.ActorId);

        builder.HasIndex(a => a.CreatedAt);

        builder.HasIndex(a => new
        {
            a.TargetType,
            a.TargetId
        });

        builder.HasIndex(a => a.Outcome);

        builder.HasOne(a => a.Actor).WithMany(u => u.AuditEvents).HasForeignKey(a => a.ActorId).OnDelete(DeleteBehavior.SetNull);
    }
}