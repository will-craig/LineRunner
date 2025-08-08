using System.Net.Http.Json;
using System.Text.Json;
using FluentAssertions;
using LineRunner.API;
using Microsoft.AspNetCore.Mvc.Testing;

namespace LineRunner.Tests.Integration.GraphTests;

public class TalkToNpcTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task TalkToNpc_ReturnsExpectedReply()
    {
        // Arrange: GraphQL query as a raw string
        var query = @"
            mutation {
                talkToNpc(input: {
                    playerId: ""aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"",
                    npcId: ""bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"",
                    message: ""Hello!""
                }) {
                    reply
                    suggestions
                    npcMood
                    contextId
                }
            }
        ";

        var requestContent = JsonContent.Create(new { query });

        // Act
        var response = await _client.PostAsync("/graphql", requestContent);

        // Assert
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();

        var doc = JsonDocument.Parse(json);
        var root = doc.RootElement.GetProperty("data").GetProperty("talkToNpc");

        root.GetProperty("reply").GetString().Should().Contain("Echo");
        root.GetProperty("npcMood").GetString().Should().Be("Curious");
        root.GetProperty("suggestions").GetArrayLength().Should().BeGreaterThan(0);
    }
}