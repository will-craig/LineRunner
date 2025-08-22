using LineRunner.API.Graph.Queries;
using LineRunner.API.Graph.Types;

namespace LineRunner.API.Graph.Mutations;

public interface IDialogMutations
{
    public Task<NpcReply> TalkToNpc(TalkToNpcQuery input);
    //public Task<bool> ResetConversationHistoryAsync(Guid playerId, Guid npcId);
}