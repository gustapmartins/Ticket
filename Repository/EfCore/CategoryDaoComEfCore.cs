using Ticket.Data;
using Ticket.DTO.Category;
using Ticket.Model;

namespace Ticket.Repository.EfCore;

public class CategoryDaoComEfCore: ICategoryDao
{
    public readonly TicketContext _ticketContext;

    public CategoryDaoComEfCore(TicketContext ticketContext)
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

    public bool CategoryExistName(CategoryCreateDto categoryDto)
    {
        return _ticketContext.Categorys.Any(category => category.Name == categoryDto.Name);
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
