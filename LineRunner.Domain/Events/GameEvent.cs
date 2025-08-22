namespace LineRunner.API.Models.Events;

public class GameEvent
{
    public string EventType { get; set; } = default!;
    public string NpcName { get; set; } = default!;
    public string PlayerMessage { get; set; } = default!;
    public string NpcReply { get; set; } = default!;
    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;
}