namespace Ticket.Interface;

public interface IMessagePublisher
{
    void Publish<TResult>(TResult message);
}
