namespace FocusFlow.DAL.Repo;

public interface IChatChannelRepository
{
    Task<ChatChannel?> GetByIdAsync(int id);
    Task<IEnumerable<ChatChannel>> GetByTeamIdAsync(int teamId);
    Task<ChatChannel?> GetByNameAsync(int teamId, string name);
    Task<bool> ExistsAsync(int teamId, string name);
    Task AddAsync(ChatChannel channel);
    void Update(ChatChannel channel);
    void Delete(ChatChannel channel);
    Task<bool> SaveChangesAsync();
}
