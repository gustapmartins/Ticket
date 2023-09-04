using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ticket.Data;
using Ticket.DTO.Show;
using Ticket.ExceptionFilter;
using Ticket.Interface;
using Ticket.Model;

namespace Ticket.Service;

public class ShowService: IShowService
{
    private readonly TicketContext _ticketContext;
    private readonly IMapper _mapper;

    public ShowService(TicketContext ticketContext, IMapper mapper)
    {
        _ticketContext = ticketContext;
        _mapper = mapper;
    }

    public List<Show> FindAll()
    {
        try
        {
            var find = _ticketContext.Shows.ToList();

            if (find.Count == 0)
            {
                throw new StudentNotFoundException("The list is empty");
            }
            return find;
        }
        catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }

    public Show FindId(int id)
    {
        try
        {
            var show = _ticketContext.Shows.FirstOrDefault(filme => filme.Id == id);

            if (show == null)
            {
                throw new StudentNotFoundException("The list is empty");
            }
            return show;
        }
        catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }

    public async Task<ShowCreateDto> CreateShow(ShowCreateDto showDto)
    {
        var category = _ticketContext.Categorys.FirstOrDefault(filme => filme.Name == showDto.Category);

        if (category == null)
        {
            throw new StudentNotFoundException("A categoria especificada não existe.");
        }
        // Crie o novo Show com a categoria existente
        var show = new Show
        {
            Name = showDto.Name,
            Description = showDto.Description,
            Date = showDto.Date,
            Local = showDto.Local,
            Category = category // Associe a categoria existente ao Show
        };

        _ticketContext.Shows.Add(show);
        _ticketContext.SaveChanges();
        return showDto;
    }
}
