using Ticket.Interface;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;

namespace Ticket.Service;

public class MessagePublisher : IMessagePublisher
{
    private readonly IConnection _factory;
    private readonly IConfiguration _configuration;
    private readonly IFeatureToggleService _featureToggleService;
    private readonly IModel _channel;

    public MessagePublisher(IConfiguration configuration, IFeatureToggleService featureToggleService)
    {
        _configuration = configuration;
        _factory = new ConnectionFactory()
        {
            HostName = _configuration["RabbitMQ:Host"],
            Port = int.Parse(_configuration["RabbitMQ:Port"]),
        }.CreateConnection();

        _channel = _factory.CreateModel();
        _featureToggleService = featureToggleService;
    }


    public void Publish<TResult>(TResult message)
    {
        string FT_RABBITMQ = "FT_RABBITMQ_TICKETS";

        var cacheHabiliy = _featureToggleService.FeatureToggleActive(FT_RABBITMQ);

        if (cacheHabiliy)
        {
            _channel.QueueDeclare(queue: Commons.Constants.RABBITMQ_TICKETS, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var result = JsonConvert.SerializeObject(message);

            var body = Encoding.UTF8.GetBytes(result);

            _channel.BasicPublish(exchange: string.Empty, routingKey: Commons.Constants.RABBITMQ_TICKETS, basicProperties: null, body: body);
        }
    }
}