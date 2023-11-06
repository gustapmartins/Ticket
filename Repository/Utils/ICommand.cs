namespace Ticket.Repository.Utils;

public interface ICommand<T>
{
    List<T> FindAll();

    Task<T> FindId(int Id);

    void Add(T category);

    void Remove(T category);

    void SaveChanges();
}
