using Ticket.ExceptionFilter;
using Ticket.Repository.Dao;
using Ticket.Validation;
using Ticket.DTO.Ticket;
using Ticket.Interface;
using Ticket.Model;
using AutoMapper;

namespace Ticket.Service;

public class TicketService: TicketBase, ITicketService
{
    private readonly IMapper _mapper;
    private readonly ITicketDao _ticketDao;
    private readonly IMessagePublisher _messagePublisher;
    public TicketService(
        ITicketDao ticketDao, 
        IMapper mapper,
        IMessagePublisher messagePublisher)
    {
        _mapper = mapper;
        _ticketDao = ticketDao;
        _messagePublisher = messagePublisher;
    }

    public List<Tickets> FindAllTicket()
    {
        try
        {
            List<Tickets> ticket = _ticketDao.FindAll();

            if (ticket.Count == 0)
            {
                throw new StudentNotFoundException("The list is empty");
            }

            return ticket;
        }
        catch (Exception ex)
        {
            if (ex is StudentNotFoundException)
            {
                throw;
            }
          
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }

    public Tickets FindIdTicket(string id)
    {
        return HandleErrorAsync(() => _ticketDao.FindId(id));
    }
   
    public TicketCreateDto CreateTicket(TicketCreateDto ticketDto)
    {
        Show show = HandleErrorAsync(() => _ticketDao.FindByShowName(ticketDto.ShowName));

        Tickets ticket = _mapper.Map<Tickets>(ticketDto);

        ticket.Show = show;

        _ticketDao.Add(ticket);

        return ticketDto;
    }

    public Tickets DeleteTicket(string Id)
    {
        var ticket = HandleErrorAsync(() => _ticketDao.FindId(Id));

        _ticketDao.Remove(ticket);

        return ticket;
    }

    public Tickets UpdateTicket(string Id, TicketUpdateDto ticketDto)
    {
        var ticket = HandleErrorAsync(() => _ticketDao.FindId(Id));

        _ticketDao.Update(ticket, ticketDto);

        return ticket;
    }
}
