using Ticket.Interface;
using RabbitMQ.Client;
using System.Text;

namespace Ticket.Service;

public class MessagePublisher : IMessagePublisher
{
    private readonly ConnectionFactory _connectionFactory;

    public MessagePublisher(string hostName)
    {
        _connectionFactory = new ConnectionFactory
        {
            HostName = hostName,
        };
    }

    public void Publish(string message)
    {
        var channel = _connectionFactory.CreateConnection().CreateModel();

        channel.QueueDeclare(queue: Commons.Constants.RABBITMQ_TICKETS, durable: false, exclusive: false, autoDelete: false, arguments: null);

        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: string.Empty, routingKey: Commons.Constants.RABBITMQ_TICKETS, basicProperties: null, body: body);
    }
}