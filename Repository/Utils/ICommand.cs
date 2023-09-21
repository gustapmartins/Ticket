namespace Ticket.Repository.Utils;

public interface ICommand<T>
{
    void Add(T category);

    void Remove(T category);

    void SaveChanges();
}
