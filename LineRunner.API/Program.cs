using Azure;
using LineRunner.API.Graph.Mutations;
using LineRunner.API.Graph.Queries;
using LineRunner.Application.Configuration;
using LineRunner.Application.Services;
using Microsoft.Extensions.Options;
using Azure.AI.OpenAI;
using LineRunner.API.Services;

namespace LineRunner.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Setup Kafka configuration
        builder.Services.AddOptions<KafkaOptions>()
            .Bind(builder.Configuration.GetSection("Kafka"))
            .ValidateOnStart();
        builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<KafkaOptions>>().Value);
        builder.Services.AddSingleton<KafkaProducer>();
        builder.Services.AddHostedService<KafkaConsumer>();
        
        //Building the GraphQL server
        builder.Services
            .AddGraphQLServer()
            .AddQueryType<TalkToNpcQuery>()
            .AddMutationType<DialogMutations>();

        //AzureOpenAI client
        builder.Services.AddOptions<AzureAiOptions>()
            .Bind(builder.Configuration.GetSection("AzureOpenAi"))
            .ValidateOnStart();
        
        builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<AzureAiOptions>>().Value);
        builder.Services.AddSingleton<AzureOpenAIClient>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<AzureAiOptions>>().Value;
            return new(
                new Uri(options.Endpoint),
                new AzureKeyCredential(options.Key));
        });
        
        builder.Services.AddScoped<IOpenAiDialogService, OpenAiDialogService>();
        
        var app = builder.Build();
        app.MapGraphQL();
        app.Run();
        
    }
}