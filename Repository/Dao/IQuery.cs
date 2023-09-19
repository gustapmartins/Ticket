namespace Ticket.Repository.Dao;

public interface IQuery<T>
{
    List<T> FindAll();

    T FindId(int Id);

    bool ExistName(string? Name);
}
