

namespace FocusFlow.DAL.Database;

public class FocusFlowDbContext : DbContext
{
    public FocusFlowDbContext(DbContextOptions<FocusFlowDbContext> options) : base(options) { }
    public DbSet<User> Users => Set<User>();
    public DbSet<UserSession> UserSessions => Set<UserSession>();
    public DbSet<AuthToken> AuthTokens => Set<AuthToken>();
    public DbSet<AuditEvent> AuditEvents => Set<AuditEvent>();
    public DbSet<ChatChannel> ChatChannels => Set<ChatChannel>();
    public DbSet<ChatMessage> ChatMessages => Set<ChatMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FocusFlowDbContext).Assembly);
    }
}