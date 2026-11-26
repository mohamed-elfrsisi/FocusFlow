
namespace FocusFlow.DAL.Entities;

public class User : BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Email { get; set; } = string.Empty;
    public string Nickname { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    
    // store secure password hash
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public UserStatus Status { get; set; }
    public bool EmailVerified { get; set; }
    public string Timezone { get; set; } = "UTC";
    public ProfileVisibility ProfileVisibility { get; set; }
    public string? AboutMe { get; set; }
    public byte WeekStart { get; set; } = 1; //0 = Sunday, 1 = Monday
    
    public DateTime? DeletionRequestedAt { get; set; }
    public DateTime? ScheduledDeletionAt { get; set; }


    public ICollection<UserSession> Sessions { get; set; } = new List<UserSession>();

    public ICollection<AuthToken> AuthTokens { get; set; } =new List<AuthToken>();

    public ICollection<AuditEvent> AuditEvents { get; set; }  = new List<AuditEvent>();

    public ICollection<ChatMessage> SentMessages { get; set; }= new List<ChatMessage>();

    public ICollection<ChatMessage> DeletedMessages { get; set; }= new List<ChatMessage>();
}