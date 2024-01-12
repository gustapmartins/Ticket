namespace Ticket.Repository.Utils;

public interface ICommand<T>
{
    List<T> FindAll();

    T FindId(string Id);

    void Add(T addObject);

    void Remove(T removeObject);

    void SaveChanges();
}
