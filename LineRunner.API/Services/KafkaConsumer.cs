using Confluent.Kafka;
using LineRunner.Application.Configuration;
using Microsoft.Extensions.Options;

namespace LineRunner.API.Services;

public class KafkaConsumer(ILogger<KafkaConsumer> logger, IOptions<KafkaOptions> opts) : BackgroundService
{
    private readonly KafkaOptions _opts = opts.Value;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var conf = new ConsumerConfig
        {
            BootstrapServers = _opts.BootstrapServers,
            GroupId = "linerunner-consumer",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = true
        };

        using var consumer = new ConsumerBuilder<string, string>(conf).Build();
        consumer.Subscribe(_opts.Topic);

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var cr = consumer.Consume(stoppingToken);
                // This is just for testing will create a seperate consumer service later that will record in the database when that exists
                logger.LogInformation("Kafka event: key={Key} value={Value}", cr.Message.Key, cr.Message.Value);
                
            }
        }
        catch (OperationCanceledException) { /* shutting down */ }
        finally
        {
            consumer.Close();
        }

        await Task.CompletedTask;
    }
}