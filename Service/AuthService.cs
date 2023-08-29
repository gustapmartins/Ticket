using AutoMapper;
using Ticket.Data;
using Ticket.ExceptionFilter;
using Ticket.Model;

namespace Ticket.Service;

public class AuthService
{
    private readonly TicketContext _ticketContext;
    private readonly IMapper _mapper;
    public AuthService(TicketContext ticketContext, IMapper mapper)
    {
        _ticketContext = ticketContext;
        _mapper = mapper;
    }

    public List<Category> FindAll()
    {
        try
        {
            var find = _ticketContext.Categorys.ToList();

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
