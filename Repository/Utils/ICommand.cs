namespace Ticket.Repository.Utils;

public interface ICommand<T>
{
    List<T> FindAll();

    T FindId(string Id);

    void Add(T category);

    void Remove(T category);

    void SaveChanges();
}
