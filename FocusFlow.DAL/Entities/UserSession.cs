using System.ComponentModel.DataAnnotations.Schema;

namespace FocusFlow.DAL.Entities;

public class UserSession
{
    public int Id { get; set; }

    [ForeignKey("User")]
    public string UserId { get; set; } = string.Empty;
    public string RefreshHash { get; set; } = string.Empty;
    public string? Device { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastSeenAt { get; set; } = DateTime.UtcNow;
    public DateTime? RevokedAt { get; set; }
    public string? CreatedById { get; set; }
    public User User { get; set; } = null!;
}