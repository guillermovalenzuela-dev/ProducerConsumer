using ApiProducer.Services.Interfaces;
using Confluent.Kafka;

namespace ApiProducer.Services;

public class ProducerService : IProducerService
{
    private readonly ProducerConfig config = default!;
    private readonly ILogger<ProducerService> logger = default!;
    private readonly IProducer<int, string> producer = default!;
    private readonly ProducerBuilder<int, string> builder = default!;
    public string Topic { get; private set; }

    public ProducerService(ProducerConfig config, ILogger<ProducerService> logger)
    {
        this.config = config;
        this.logger = logger;
        Topic = "";
        builder = new ProducerBuilder<int, string>(config);
        producer = builder.Build();
    }
    public async Task Send(string message)
    {
        await Task.Run(() =>
        {
            var messagePacket = new Message<int, string>() { Key = 1, Value = message };
            producer.ProduceAsync(Topic, messagePacket, CancellationToken.None);

        });
    }

    public async Task SetTopic(string topic)
    {
        await Task.Run(() => { Topic = topic; });
    }
}
