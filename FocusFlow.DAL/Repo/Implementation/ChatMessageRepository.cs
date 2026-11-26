using FocusFlow.DAL.Database;
using FocusFlow.DAL.Entities;
using FocusFlow.DAL.Repo.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.DAL.Repo.Implementation;

public class ChatMessageRepository : IChatMessageRepository
{
    private readonly FocusFlowDbContext DB;

    public ChatMessageRepository(FocusFlowDbContext Db)
    {
        DB = Db;
    }

    public async Task<ChatMessage?> GetByIdAsync(int id)
    {
        return await DB.ChatMessages
            .FirstOrDefaultAsync(m => m.Id == id && m.DeletedAt == null);
    }

    public async Task<IEnumerable<ChatMessage>> GetByChannelIdAsync(int channelId)
    {
        return await DB.ChatMessages
            .Where(m => m.ChannelId == channelId && m.DeletedAt == null)
            .OrderBy(m => m.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<ChatMessage>> GetBySenderIdAsync(string senderId)
    {
        return await DB.ChatMessages
            .Where(m => m.SenderId == senderId && m.DeletedAt == null)
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await DB.ChatMessages
            .AnyAsync(m => m.Id == id && m.DeletedAt == null);
    }

    public async Task AddAsync(ChatMessage message)
    {
        await DB.ChatMessages.AddAsync(message);
    }

    public void Update(ChatMessage message)
    {
        DB.ChatMessages.Update(message);
    }

    /// <summary>
    /// Soft deletes a chat message instead of permanently removing it.
    /// </summary>
    /// <param name="message">The chat message to soft delete.</param>
    public void Delete(ChatMessage message)
    {
        message.DeletedAt = DateTime.UtcNow;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await DB.SaveChangesAsync() > 0;
    }
}