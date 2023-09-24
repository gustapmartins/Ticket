using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Ticket.Data;
using Ticket.DTO.Ticket;
using Ticket.ExceptionFilter;
using Ticket.Interface;
using Ticket.Model;
using Ticket.Repository.Dao;

namespace Ticket.Service;

public class TicketService: ITicketService
{
    private readonly IMapper _mapper;
    private readonly ITicketDao _ticketDao;

    public TicketService(ITicketDao ticketDao, IMapper mapper)
    {
        _mapper = mapper;
        _ticketDao = ticketDao;
    }

    public List<Tickets> FindAll()
    {
        try
        {
            var ticket = _ticketDao.FindAll();

            if (ticket.Count == 0)
            {
                throw new StudentNotFoundException("The list is empty");
            }

            return ticket;
        }
        catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }

    public Tickets FindId(int id)
    {
        try
        {
            var ticket = _ticketDao.FindId(id);

            if (ticket == null)
            {
                throw new StudentNotFoundException("The list is empty");
            }
            return ticket;
        }
        catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }

    public TicketCreateDto CreateTicket(TicketCreateDto ticketDto)
    {
        try
        {
            var show = _ticketDao.FindByShowId(ticketDto.ShowId);

            if (show == null)
            {
                throw new StudentNotFoundException("A categoria especificada não existe.");
            }

            decimal totalPrice = show.Price * ticketDto.QuantityTickets;

            var tickets = new Tickets
            {
                Price = totalPrice,
                QuantityTickets = ticketDto.QuantityTickets,
                Show = show,
            };

            _ticketDao.Add(tickets);
            return ticketDto;
        }
        catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }

    public Tickets DeleteTicket(int Id)
    {
        try
        {
            var ticket = _ticketDao.FindId(Id);

            if (ticket == null)
            {
                throw new StudentNotFoundException("This value does not exist");
            }
            _ticketDao.Remove(ticket);
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
            var ticket = _ticketDao.FindId(id);

            if (ticket == null)
            {
                throw new StudentNotFoundException("This value does not exist");
            }

            var ticketView = _mapper.Map<TicketUpdateDto>(ticket);

            ticketDto.ApplyTo(ticketView);

            _mapper.Map(ticketView, ticket);
            _ticketDao.SaveChanges();
            return ticketView;
        }
        catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }
}
