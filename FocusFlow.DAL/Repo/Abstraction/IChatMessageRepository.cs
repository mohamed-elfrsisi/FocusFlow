namespace FocusFlow.DAL.Repo;

public interface IChatMessageRepository
{
    Task<ChatMessage?> GetByIdAsync(int id);
    Task<IEnumerable<ChatMessage>> GetByChannelIdAsync(int channelId);
    Task<IEnumerable<ChatMessage>> GetBySenderIdAsync(string senderId);
    Task<bool> ExistsAsync(int id);
    Task AddAsync(ChatMessage message);
    void Update(ChatMessage message);
    void Delete(ChatMessage message);
    Task<bool> SaveChangesAsync();
}