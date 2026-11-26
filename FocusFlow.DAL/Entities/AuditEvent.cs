using FocusFlow.DAL.Entities.Enums;
namespace FocusFlow.DAL.Entities;

public class AuditEvent
{
    public long Id { get; set; }
    public string? ActorId { get; set; }
    
    public string Action { get; set; } = string.Empty;
    public string TargetType { get; set; } = string.Empty;
    public string? TargetId { get; set; }
    public string? Description { get; set; }
    public AuditOutcome Outcome { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public int? TeamId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User? Actor { get; set; }
}