
namespace FocusFlow.DAL.Repo.Implementation;

public class UserSessionRepository : IUserSessionRepository
{
    private readonly FocusFlowDbContext DB;

    public UserSessionRepository(FocusFlowDbContext Db)
    {
        DB= Db;
    }

    public async Task<UserSession?> GetByIdAsync(int id)
    {
        return await DB.UserSessions
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<UserSession>> GetByUserIdAsync(string userId)
    {
        return await DB.UserSessions
            .Where(s => s.UserId == userId)
            .OrderByDescending(s => s.LastSeenAt)
            .ToListAsync();
    }

    public async Task<UserSession?> GetByRefreshHashAsync(string refreshHash)
    {
        return await DB.UserSessions
            .FirstOrDefaultAsync(s =>
                s.RefreshHash == refreshHash &&
                s.RevokedAt == null);
    }

    public async Task<IEnumerable<UserSession>> GetActiveSessionsByUserIdAsync(string userId)
    {
        return await DB.UserSessions
            .Where(s =>
                s.UserId == userId &&
                s.RevokedAt == null)
            .OrderByDescending(s => s.LastSeenAt)
            .ToListAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await DB.UserSessions
            .AnyAsync(s => s.Id == id);
    }

    public async Task AddAsync(UserSession session)
    {
        await DB.UserSessions.AddAsync(session);
    }

    public void Update(UserSession session)
    {
        session.UpdatedAt = DateTime.UtcNow;
        DB.UserSessions.Update(session);
    }

    /// <summary>
    /// Revokes a user session without permanently deleting its record.
    /// </summary>
    /// <param name="session">The session to revoke.</param>
    public void Revoke(UserSession session)
    {
        session.RevokedAt = DateTime.UtcNow;
        session.UpdatedAt = DateTime.UtcNow;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await DB.SaveChangesAsync() > 0;
    }
}
