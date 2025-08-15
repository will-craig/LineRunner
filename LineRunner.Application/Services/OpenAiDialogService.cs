using Azure.AI.OpenAI;
using LineRunner.Application.Configuration;
using OpenAI.Chat;

namespace LineRunner.Application.Services;

public interface IOpenAiDialogService
{
    Task<string> GenerateNpcReplyAsync(string npcPersona, string playerMessage);
}

public class OpenAiDialogService(AzureOpenAIClient client, AzureAiOptions azureAiOptions) : IOpenAiDialogService
{
    private readonly ChatClient _chatClient = client.GetChatClient(azureAiOptions.DeploymentModel);

    public async Task<string> GenerateNpcReplyAsync(string npcPersona, string playerMessage)
    {
        var chatOptions = new ChatCompletionOptions
        {
            MaxOutputTokenCount = 4096,
            Temperature = 1.0f,
            TopP = 1.0f
        };

        List<ChatMessage> messages = new List<ChatMessage>()
        {
            new SystemChatMessage("You are an NPC with the persona: " + npcPersona),
            new UserChatMessage(playerMessage),
        };

        var response = await _chatClient.CompleteChatAsync(messages, chatOptions);
        return response.Value.Content[0].Text;
    }
}     