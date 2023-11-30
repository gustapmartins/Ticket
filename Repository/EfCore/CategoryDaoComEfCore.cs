using Ticket.Data;
using Ticket.DTO.Category;
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
        return _ticketContext.Categorys.OrderByDescending(category => category.Name).Distinct().ToList();
    }

    public Category FindId(string Id)
    {
        return _ticketContext.Categorys.FirstOrDefault(category => category.Id == Id)!;
    }

    public Category FindByName(string Name)
    {
        return _ticketContext.Categorys.FirstOrDefault(category => category.Name == Name)!;
    }

    public void Add(Category category)
    {
        _ticketContext.Categorys.Add(category);
        _ticketContext.SaveChanges();
    }

    public void Remove(Category category)
    {
        _ticketContext.Categorys.Remove(category);
        _ticketContext.SaveChanges();
    }

    public void SaveChanges()
    {
        _ticketContext.SaveChanges();
    }

    public void Update(Category category, CategoryUpdateDto updatedCategoryDto)
    {
        if (updatedCategoryDto.Name != null)
        {
            category.Name = updatedCategoryDto.Name;
        }
        if (updatedCategoryDto.Description != null)
        {
            category.Description = updatedCategoryDto.Description;
        }

        _ticketContext.SaveChanges();
    }
}
