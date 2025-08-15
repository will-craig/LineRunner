using Azure;
using LineRunner.API.Graph.Mutations;
using LineRunner.API.Graph.Queries;
using LineRunner.Application.Configuration;
using LineRunner.Application.Services;
using Microsoft.Extensions.Options;
using Azure.AI.OpenAI;

namespace LineRunner.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.Configure<AzureAiOptions>(builder.Configuration.GetSection("AzureAi"));
        
        //Building the GraphQL server
        builder.Services
            .AddGraphQLServer()
            .AddQueryType<TalkToNpcQuery>()
            .AddMutationType<DialogMutations>();

        //AzureOpenAI client
        builder.Services.AddSingleton<AzureOpenAIClient>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<AzureAiOptions>>().Value;
            return new(
                new Uri(options.Endpoint),
                new AzureKeyCredential(options.ApiKey));
        });
        
        builder.Services.AddScoped<IOpenAiDialogService, OpenAiDialogService>();
        
        var app = builder.Build();
        app.MapGraphQL();
        app.Run();
        
    }
}