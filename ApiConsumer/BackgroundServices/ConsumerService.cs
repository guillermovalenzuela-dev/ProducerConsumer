using Confluent.Kafka;
using static Confluent.Kafka.ConfigPropertyNames;

namespace ApiConsumer.BackgroundServices;

public class ConsumerService : BackgroundService
{
    private readonly ConsumerConfig config = default!;
    private readonly IConsumer<int, string> consumer = default!;
    private readonly ConsumerBuilder<int, string> builder = default!;
    private readonly ILogger<ConsumerService> logger = default!;
    public string Topic { get; private set; }
    public ConsumerService(ConsumerConfig config,ILogger<ConsumerService> logger)
    {
        this.config = config;
        this.logger = logger;
        Topic = "MESSAGES";
        builder = new ConsumerBuilder<int, string>(config);
        consumer = builder.Build();
    }
    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            consumer.Subscribe(Topic);
            try
            {
                logger.LogInformation($"Trying to consume events on topic '{Topic}'...");
                var result = consumer.Consume(stoppingToken);
                await Task.Run(() => { logger.LogInformation(result.Message.Value); });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to consume events on topic '{Topic}': {ex.Message}");
                Thread.Sleep(10000);
            }
        }
    }
}
