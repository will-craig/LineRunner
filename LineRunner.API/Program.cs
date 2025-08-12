using Azure;
using LineRunner.API.Graph.Mutations;
using LineRunner.API.Graph.Queries;
using LineRunner.Application.Configuration;
using LineRunner.Application.Services;
using Microsoft.Extensions.Options;
using Azure;
using Azure.Identity;
using Azure.AI.Projects;
using Azure.AI.Inference;
using Azure.Core;

namespace LineRunner.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //Building the GraphQL server
        builder.Services
            .AddGraphQLServer()
            .AddQueryType<TalkToNpcQuery>()
            .AddMutationType<DialogMutations>();
        
        builder.Services.Configure<AzureAiOptions>(builder.Configuration.GetSection("AzureAi"));

        //Building the OpenAI client
        builder.Services.AddSingleton(sp =>
        {
            var options = sp.GetRequiredService<IOptions<AzureAiOptions>>().Value;

            return new AIProjectClient(
                new Uri(options.Endpoint),
                new DefaultAzureCredential()
            );
        });
        
        builder.Services.AddScoped<IOpenAiDialogService, OpenAiDialogService>();
        
        var app = builder.Build();
        app.MapGraphQL();
        app.Run();
    }
}