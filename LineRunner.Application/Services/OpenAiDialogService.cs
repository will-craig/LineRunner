using Azure;
using Azure.Identity;
using Azure.AI.Projects;
using Azure.AI.Inference;

namespace LineRunner.Application.Services;

public interface IOpenAiDialogService
{
    Task<string> GenerateNpcReplyAsync(string npcPersona, string playerMessage);
}

public class OpenAiDialogService(AIProjectClient client) : IOpenAiDialogService
{
    private readonly ChatCompletionsClient _chatClient = client.GetChatCompletionsClient();
    public async Task<string> GenerateNpcReplyAsync(string npcPersona, string playerMessage)
    {
        var chatOptions = new ChatCompletionsOptions
        {
            Messages =
            {
                new ChatRequestSystemMessage("You are: " + npcPersona),
                new ChatRequestUserMessage(playerMessage)
            },
            Model = "gpt-4o",
        };
        
        var response = await _chatClient.CompleteAsync(chatOptions);
        
        return response.Value.Content;
    }
}     