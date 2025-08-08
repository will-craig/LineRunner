using LineRunner.API.Graph.Queries;
using LineRunner.API.Graph.Types;

namespace LineRunner.API.Graph.Mutations;

public class DialogMutations
{
    public async Task<NpcReply> TalkToNpc(TalkToNpcQuery input)
    {
        // Stub for now
        return new NpcReply
        {
            Reply = $"Echo: {input.Message}",
            Suggestions = new List<string> { "Tell me more", "Who are you?", "Goodbye" },
            NpcMood = "Curious",
            ContextId = Guid.NewGuid()
        };
    }
}