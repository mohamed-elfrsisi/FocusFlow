using FocusFlow.DAL.Entities;

namespace FocusFlow.DAL.Repo.Abstraction;

public interface IUserSessionRepository
{
    Task<UserSession?> GetByIdAsync(int id);
    Task<IEnumerable<UserSession>> GetByUserIdAsync(string userId);
    Task<UserSession?> GetByRefreshHashAsync(string refreshHash);
    Task<IEnumerable<UserSession>> GetActiveSessionsByUserIdAsync(string userId);
    Task<bool> ExistsAsync(int id);
    Task AddAsync(UserSession session);
    void Update(UserSession session);
    void Revoke(UserSession session);
    Task<bool> SaveChangesAsync();
}