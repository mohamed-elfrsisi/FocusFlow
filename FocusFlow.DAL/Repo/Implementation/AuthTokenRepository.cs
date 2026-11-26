

namespace FocusFlow.DAL.Repo.Implementation;

public class AuthTokenRepository : IAuthTokenRepository
{
    private readonly FocusFlowDbContext DB;

    public AuthTokenRepository(FocusFlowDbContext Db)
    {
        DB = Db;
    }

    public async Task<AuthToken?> GetByIdAsync(int id)
    {
        return await DB.AuthTokens.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<AuthToken?> GetByTokenHashAsync(string tokenHash)
    {
        return await DB.AuthTokens
            .FirstOrDefaultAsync(t =>
                t.TokenHash == tokenHash &&
                t.RevokedAt == null &&
                t.UsedAt == null &&
                t.ExpiresAt > DateTime.UtcNow);
    }

    public async Task<IEnumerable<AuthToken>> GetByUserIdAsync(string userId)
    {
        return await DB.AuthTokens
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task<bool> ExistsAsync(string tokenHash)
    {
        return await DB.AuthTokens
            .AnyAsync(t => t.TokenHash == tokenHash);
    }

    public async Task AddAsync(AuthToken authToken)
    {
        await DB.AuthTokens.AddAsync(authToken);
    }

    public void Update(AuthToken authToken)
    {
        DB.AuthTokens.Update(authToken);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await DB.SaveChangesAsync() > 0;
    }
}