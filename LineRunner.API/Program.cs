using LineRunner.API.Graph.Mutations;
using LineRunner.API.Graph.Queries;

namespace LineRunner.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddGraphQLServer()
            .AddQueryType<TalkToNpcQuery>()
            .AddMutationType<DialogMutations>();

        var app = builder.Build();
        app.MapGraphQL();
        app.Run();
    }
}