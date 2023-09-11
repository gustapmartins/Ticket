using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Ticket.Data;
using Ticket.DTO.Ticket;
using Ticket.ExceptionFilter;
using Ticket.Model;

namespace Ticket.Service;

public class TicketService
{
    private readonly TicketContext _ticketContext;
    private readonly IMapper _mapper;

    public TicketService(TicketContext ticketContext, IMapper mapper)
    {
        _ticketContext = ticketContext;
        _mapper = mapper;
    }

    public List<Tickets> FindAll()
    {
        try
        {
            var find = _ticketContext.Tickets.ToList();

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

    public Tickets FindId(int id)
    {
        var ticket = _ticketContext.Tickets.FirstOrDefault(filme => filme.Id == id);

        if (ticket == null)
        {
            throw new StudentNotFoundException("The list is empty");
        }
        return ticket;
    }

    public IEnumerable<Tickets> FindAllTicket()
    {
        try
        {
            var ticketFind = _ticketContext.Tickets.ToList();

            if (ticketFind.Count == 0)
            {
                throw new StudentNotFoundException("The list is empty");
            }
            return ticketFind;
        }
        catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }

    public TicketCreateDto CreateTicket(TicketCreateDto ticketDto)
    {
        var show = _ticketContext.Shows.FirstOrDefault(ticket => ticket.Id == ticketDto.ShowId);

        if (show == null)
        {
            throw new StudentNotFoundException("A categoria especificada não existe.");
        }

        var tickets = new Tickets
        {
            Price = ticketDto.Price,
            QuantityTickets = ticketDto.Quantity,
            Show = show,
        };

        _ticketContext.Tickets.Add(tickets);
        _ticketContext.SaveChanges();
        return ticketDto;
    }

    public Tickets DeleteTicket(int id)
    {
        try
        {
            var ticket = _ticketContext.Tickets.FirstOrDefault(ticket => ticket.Id == id);
            if (ticket == null)
            {
                throw new StudentNotFoundException("This value does not exist");
            }
            _ticketContext.Remove(ticket);
            _ticketContext.SaveChanges();
            return ticket;
        }
        catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }

    public TicketUpdateDto UpdateTicket(int id, JsonPatchDocument<TicketUpdateDto> ticketDto)
    {
        try
        {
            var ticket = _ticketContext.Tickets.FirstOrDefault(category => category.Id == id);

            if (ticket == null)
            {
                throw new StudentNotFoundException("This value does not exist");
            }

            var ticketView = _mapper.Map<TicketUpdateDto>(ticket);

            ticketDto.ApplyTo(ticketView);

            _mapper.Map(ticketView, ticket);
            _ticketContext.SaveChanges();
            return ticketView;
        }
        catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }
}
