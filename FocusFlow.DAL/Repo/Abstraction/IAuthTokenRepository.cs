namespace FocusFlow.DAL.Repo.Abstraction;

public interface IAuthTokenRepository
{
    Task<AuthToken?> GetByIdAsync(int id);
    Task<AuthToken?> GetByTokenHashAsync(string tokenHash);
    Task<IEnumerable<AuthToken>> GetByUserIdAsync(string userId);
    Task<bool> ExistsAsync(string tokenHash);
    Task AddAsync(AuthToken authToken);
    void Update(AuthToken authToken);
    Task<bool> SaveChangesAsync();
}