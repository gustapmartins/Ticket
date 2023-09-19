namespace Ticket.Repository.Dao;

public interface ICommand<T>
{
    void Add(T category);

    void Remove(T category);

    void SaveChanges();
}
