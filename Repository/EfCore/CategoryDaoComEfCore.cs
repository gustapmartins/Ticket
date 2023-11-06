using Microsoft.EntityFrameworkCore;
using Ticket.Data;
using Ticket.DTO.Category;
using Ticket.ExceptionFilter;
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

    public async Task<Category> FindId(int Id)
    {
        return await _ticketContext.Categorys.FirstAsync(category => category.Id == Id);
    }

    public Category FindByName(string Name)
    {
        var response = _ticketContext.Categorys.FirstOrDefault(category => category.Name == Name);

        if (response == null) throw new StudentNotFoundException("this value does not exist");

        return response;
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
