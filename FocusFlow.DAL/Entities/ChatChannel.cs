using FocusFlow.DAL.Entities.Enums;
namespace FocusFlow.DAL.Entities;

public class ChatChannel : BaseEntity
{
    public int Id { get; set; }
    public int TeamId { get; set; }
    public string Name { get; set; } = string.Empty;

    public ChatChannelType Type { get; set; }
    public string? Description { get; set; }

    public bool IsDefault { get; set; }

    public ICollection<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
}