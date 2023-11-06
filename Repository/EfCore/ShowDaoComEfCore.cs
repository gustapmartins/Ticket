using Microsoft.EntityFrameworkCore;
using Ticket.Repository.Dao;
using Ticket.DTO.Show;
using Ticket.Model;
using Ticket.Data;
using Ticket.ExceptionFilter;

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

    public async Task<Show> FindId(int Id) 
    {
        return await _ticketContext.Shows.FirstAsync(show => show.Id == Id);
    }

    public async Task<List<Show>> FindByShowNameList(string name)
    {
        return await Task.Run(() =>
        {
            return _ticketContext.Shows.Where(show => show.Name.StartsWith(name)).ToList();
        });
    }

    public Category FindByCategoryName(string Name)
    {
        var response = _ticketContext.Categorys.FirstOrDefault(category => category.Name == Name);

        if (response == null) throw new StudentNotFoundException("this value does not exist");

        return response;
    }

    public Show FindByName(string Name)
    {
        var response = _ticketContext.Shows.FirstOrDefault(show => show.Name == Name);

        if (response == null) throw new StudentNotFoundException("this value does not exist");

        return response;
    }

    public void Add(Show show)
    {
        _ticketContext.Shows.Add(show);
        _ticketContext.SaveChanges();
    }

    public void Remove(Show show)
    {
        _ticketContext.Remove(show);
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
        if(showUpdateDto.Local != null)
        {
            show.Local = showUpdateDto.Local;
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
