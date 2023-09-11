
using Ticket.Model;

namespace Ticket.Data;

public class CategoryDao
{
    public readonly TicketContext _ticketContext;

    public CategoryDao(TicketContext ticketContext)
    {
        _ticketContext = ticketContext;
    }

    public IEnumerable<Category> FindAllCategorys()
    {
        return _ticketContext.Categorys.ToList();
    }

    public Category FindIdCategory(int Id)
    {
        return _ticketContext.Categorys.FirstOrDefault(category => category.Id == Id);
    }
}
