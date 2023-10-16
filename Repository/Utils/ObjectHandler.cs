namespace Ticket.Repository.Utils;

public interface ObjectHandler<T, U>
{
    void Update(T objectUpdate, U request);
}
