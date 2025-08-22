using Confluent.Kafka;
using Microsoft.Extensions.Options;
using System.Text.Json;
using LineRunner.API.Models;
using LineRunner.API.Models.Events;
using LineRunner.Application.Configuration;

namespace LineRunner.API.Services;

public class KafkaProducer : IAsyncDisposable
{
    private readonly IProducer<string, string> _producer;
    private readonly string _topic;

    public KafkaProducer(IOptions<KafkaOptions> options)
    {
        var cfg = options.Value;
        _topic = cfg.Topic;

        var producerConfig = new ProducerConfig
        {
            BootstrapServers = cfg.BootstrapServers,
            Acks = Acks.All,
            EnableIdempotence = true
        };

        _producer = new ProducerBuilder<string, string>(producerConfig).Build();
    }

    public async Task ProduceAsync(GameEvent evt, CancellationToken ct = default)
    {
        var key = evt.NpcName ?? "npc";
        var payload = JsonSerializer.Serialize(evt);
        await _producer.ProduceAsync(_topic, new Message<string, string> { Key = key, Value = payload }, ct);
    }

    public ValueTask DisposeAsync()
    {
        _producer.Flush(TimeSpan.FromSeconds(2));
        _producer.Dispose();
        return ValueTask.CompletedTask;
    }
}
