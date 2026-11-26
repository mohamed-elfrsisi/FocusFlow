
public class ChatMessage : BaseEntity
{
    public int Id { get; set; }
    public int ChannelId { get; set; }
    public string SenderId { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public ChatChannel Channel { get; set; } = null!;
    public User Sender { get; set; } = null!;
    public User? DeletedBy { get; set; }
}