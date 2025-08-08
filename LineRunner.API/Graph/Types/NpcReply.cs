namespace LineRunner.API.Graph.Types;

public class NpcReply
{
    public string Reply { get; set; } = null!;
    public List<string> Suggestions { get; set; } = new();
    public string? NpcMood { get; set; }
    public Guid ContextId { get; set; }
}