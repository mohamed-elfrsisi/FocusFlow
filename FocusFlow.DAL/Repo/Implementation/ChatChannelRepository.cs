
namespace FocusFlow.DAL.Repo.Implementation;

public class ChatChannelRepository : IChatChannelRepository
{
    private readonly FocusFlowDbContext DB;

    public ChatChannelRepository(FocusFlowDbContext Db)
    {
        DB = Db;
    }

    public async Task<ChatChannel?> GetByIdAsync(int id)
    {
        return await DB.ChatChannels
            .FirstOrDefaultAsync(c => c.Id == id && c.DeletedAt == null);
    }

    public async Task<IEnumerable<ChatChannel>> GetByTeamIdAsync(int teamId)
    {
        return await DB.ChatChannels
            .Where(c => c.TeamId == teamId && c.DeletedAt == null)
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    public async Task<ChatChannel?> GetByNameAsync(int teamId, string name)
    {
        return await DB.ChatChannels
            .FirstOrDefaultAsync(c =>
                c.TeamId == teamId &&
                c.Name == name &&
                c.DeletedAt == null);
    }

    public async Task<bool> ExistsAsync(int teamId, string name)
    {
        return await DB.ChatChannels
            .AnyAsync(c =>
                c.TeamId == teamId &&
                c.Name == name &&
                c.DeletedAt == null);
    }

    public async Task AddAsync(ChatChannel channel)
    {
        await DB.ChatChannels.AddAsync(channel);
    }

    public void Update(ChatChannel channel)
    {
        DB.ChatChannels.Update(channel);
    }


    public void Delete(ChatChannel channel)
    {
        channel.DeletedAt = DateTime.UtcNow;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await DB.SaveChangesAsync() > 0;
    }
}
