using AutoMapper;
using Ticket.Data;
using Ticket.ExceptionFilter;
using Ticket.Interface;
using Ticket.Model;

namespace Ticket.Service;

public class TicketService: ITicketService
{
    private readonly TicketContext _ticketContext;
    private readonly IMapper _mapper;

    public TicketService(TicketContext ticketContext, IMapper mapper)
    {
        _ticketContext = ticketContext;
        _mapper = mapper;
    }

    public IEnumerable<Tickets> FindAllTicket()
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
}
