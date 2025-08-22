using LineRunner.API.Graph.Queries;
using LineRunner.API.Graph.Types;
using LineRunner.API.Models.Events;
using LineRunner.API.Services;
using LineRunner.Application.Services;

namespace LineRunner.API.Graph.Mutations;

public class DialogMutations(IOpenAiDialogService openAIService, KafkaProducer producer) : IDialogMutations
{
    public async Task<NpcReply> TalkToNpc(TalkToNpcQuery input)
    {
        var reply = await openAIService.GenerateNpcReplyAsync(
            npcPersona: "Gandalf The Grey", // will be fetched inside the service based on npcId
            input.Message);
        
        // send latest dialogue to Kafka stream
        var evt = new GameEvent
        {
            EventType = "NpcDialogue",
            NpcName = "NPC", // will be fetched inside the service based on npcId
            PlayerMessage = input.Message,
            NpcReply = reply
        };
        
        await producer.ProduceAsync(evt);
        
        return new NpcReply
        {
            Reply = reply,
            NpcMood = "Curious", // will pull this conext from model in future
            Suggestions = new List<string> { "Tell me more", "Who are you?", "Goodbye" }, //example suggestions for now
            ContextId = Guid.NewGuid()
        };
        
    }
}