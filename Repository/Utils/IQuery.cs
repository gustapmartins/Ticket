namespace Ticket.Repository.Utils;

public interface IQuery<T>
{
    List<T> FindAll();

    T FindId(int Id);
}
