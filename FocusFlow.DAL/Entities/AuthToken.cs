using System.ComponentModel.DataAnnotations.Schema;
using FocusFlow.DAL.Entities.Enums;
namespace FocusFlow.DAL.Entities;

public class AuthToken
{
    public int Id { get; set; }

    [ForeignKey("User")]
    public string UserId { get; set; } = string.Empty;
    public string TokenHash { get; set; } = string.Empty;
    public AuthTokenType Type { get; set; }
    
    public DateTime ExpiresAt { get; set; }
    public DateTime? UsedAt { get; set; }
    public DateTime? RevokedAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public User User { get; set; } = null!;
}