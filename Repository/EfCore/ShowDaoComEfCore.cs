using Ticket.Repository.Dao;
using Ticket.DTO.Show;
using Ticket.Model;
using Ticket.Data;

namespace Ticket.Repository.EfCore;

public class ShowDaoComEfCore: IShowDao
{
    public readonly TicketContext _ticketContext;

    public ShowDaoComEfCore(TicketContext ticketContext)
    {
        _ticketContext = ticketContext;
    }

    public List<Show> FindAll()
    {
        return _ticketContext.Shows.OrderByDescending(show => show.Id).ToList();
    }

    public Show FindId(string Id) 
    {
        return _ticketContext.Shows.FirstOrDefault(show => show.Id == Id)!;
    }

    public Category FindByCategoryName(string Name)
    {
        return _ticketContext.Categorys.FirstOrDefault(category => category.Name == Name)!;
    }

    public Show FindByName(string Name)
    {
        return _ticketContext.Shows.FirstOrDefault(show => show.Name == Name)!;
    }

    public void Add(Show show)
    {
        _ticketContext.Shows.Add(show);
        _ticketContext.SaveChanges();
    }

    public void Remove(Show show)
    {
        _ticketContext.Shows.Remove(show);
        _ticketContext.SaveChanges();
    }

    public void Update(Show show, ShowUpdateDto showUpdateDto)
    {
        if (showUpdateDto.Name != null)
        {
            show.Name = showUpdateDto.Name;
        }
        if (showUpdateDto.Description != null)
        {
            show.Description = showUpdateDto.Description;
        }
        if(showUpdateDto.CategoryName != null)
        {
            Category category = FindByCategoryName(showUpdateDto.CategoryName);
            show.Category = category;
        }

        _ticketContext.SaveChanges();
    }

    public void SaveChanges()
    {
        _ticketContext.SaveChanges();
    }
}
