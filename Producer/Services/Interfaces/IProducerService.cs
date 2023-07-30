namespace Producer.Services.Interfaces;
public interface IProducerService
{
    Task Send(string message);
    Task SetTopic(string topic);
}
