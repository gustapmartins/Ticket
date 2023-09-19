using Ticket.Data;
using Ticket.Model;
using Ticket.Repository.Dao;

namespace Ticket.Repository.EfCore;

public class CategoryDaoComEfCore: ICategoryDao
{
    public readonly TicketContext _ticketContext;

    public CategoryDaoComEfCore(TicketContext ticketContext)
    {
        _ticketContext = ticketContext;
    }

    public List<Category> FindAll()
    {
        return _ticketContext.Categorys.ToList();
    }

    public Category FindId(int Id)
    {
        return _ticketContext.Categorys.FirstOrDefault(category => category.Id == Id);
    }

    public bool ExistName(string Name)
    {
        return _ticketContext.Categorys.Any(category => category.Name == Name);
    }

    public void Add(Category category)
    {
        _ticketContext.Categorys.Add(category);
        _ticketContext.SaveChanges();
    }

    public void Remove(Category category)
    {
        _ticketContext.Remove(category);
        _ticketContext.SaveChanges();
    }

    public void SaveChanges()
    {
        _ticketContext.SaveChanges();
    }
}
