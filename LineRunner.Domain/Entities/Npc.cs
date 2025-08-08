namespace LineRunner.Domain.NPCs;

public class Npc
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public string Mood { get; private set; }
    public string RelationshipWithPlayer { get; private set; }

    private List<string> _recentEvents = new();
    public IReadOnlyList<string> RecentEvents => _recentEvents.AsReadOnly();

    public string LastResponse { get; private set; }

    public Npc(string id, string name)
    {
        Id = id;
        Name = name;
        Mood = "Neutral";
        RelationshipWithPlayer = "Unknown";
    }

    public void RecordEvent(string eventType)
    {
        _recentEvents.Add(eventType);
    }

    public void SetResponse(string response)
    {
        LastResponse = response;
    }
}
