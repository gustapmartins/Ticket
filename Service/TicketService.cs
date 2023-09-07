using AutoMapper;
using System.Text;
using Ticket.Data;
using Ticket.DTO.Ticket;
using Ticket.ExceptionFilter;
using Ticket.Interface;
using Ticket.Model;
using System.Security.Cryptography;

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

    private string ComputeSHA256()
    {
        SHA256 sha256 = SHA256.Create();
        byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(string.Empty));
        return Convert.ToHexString(hashValue);
    }

    public IEnumerable<Tickets> FindAllTicket()
    {
        try
        {
            var ticketFind = _ticketContext.Tickets.ToList();

            Random random = new Random();

            // Gere um número inteiro aleatório entre 1 e 100 (inclusive)
            int numeroAleatorio = random.Next(1, 101);

            Console.WriteLine("Número aleatório: " + numeroAleatorio);

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
        var show = _ticketContext.Shows.FirstOrDefault(ticket =>
            ticket.Id == ticketDto.ShowId);

        if (show == null)
        {
            throw new StudentNotFoundException("A categoria especificada não existe.");
        }

        var ticket = new Tickets
        {
            QuantityTickets = ticketDto.QuantityTickets,
            TicketNumber = ComputeSHA256(),
            Show = show
        };
        _ticketContext.Tickets.Add(ticket);
        _ticketContext.SaveChanges();
        return ticketDto;
    }
}
