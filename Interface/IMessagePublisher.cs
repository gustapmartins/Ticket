namespace Ticket.Interface;

public interface IMessagePublisher
{
    void Publish(string message);
}
