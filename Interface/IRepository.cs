namespace Ticket.Interface;

public interface IRepository<T, U> where T : class
{

    U Add(T entity);
    U Delete(T entity);
    U Update(T entity);
    IEnumerable<U> All();
}
