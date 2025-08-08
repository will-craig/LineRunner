namespace LineRunner.API.Graph.Queries;

public record TalkToNpcQuery(
    Guid PlayerId,
    Guid NpcId,
    string Message);