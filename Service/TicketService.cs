using Ticket.ExceptionFilter;
using Ticket.Repository.Dao;
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
                throw; // Re-lança a exceção original
            }
            else
            {
                throw new StudentNotFoundException("Error in the request", ex);
            }
        }
    }

    public Tickets FindIdTicket(int id)
    {
        return HandleErrorAsync(() => _ticketDao.FindId(id));
    }
   
    public Tickets CreateTicket(TicketCreateDto ticketDto)
    {
        try
        {
            Show show = _ticketDao.FindByShowName(ticketDto.ShowName);

            if (show == null)
            {
                throw new StudentNotFoundException("the specified show does not exist");
            }

            Tickets ticket = _mapper.Map<Tickets>(ticketDto);

            ticket.Show = show;

            _ticketDao.Add(ticket);

            return ticket;
        }
        catch (Exception ex)
        {
            if( ex is StudentNotFoundException)
            {
                throw;
            }

            throw new StudentNotFoundException("Error in the request", ex);
        }
    }

    public Tickets DeleteTicket(int Id)
    {
        var ticket = HandleErrorAsync(() => _ticketDao.FindId(Id));

        _ticketDao.Remove(ticket);

        return ticket;
    }

    public Tickets UpdateTicket(int Id, TicketUpdateDto ticketDto)
    {
        try
        {
            var ticket = HandleErrorAsync(() => _ticketDao.FindId(Id));

            _ticketDao.Update(ticket, ticketDto);

            return ticket;
        }
        catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }

    public BuyTicketDto BuyTicketsAsync(BuyTicketDto buyTicket)
    {
        //busca o ticket que o usuario está informando o id
        Tickets findTicket = HandleErrorAsync(() => _ticketDao.FindId(buyTicket.TicketId));

        Users findUser = HandleErrorAsync(() => _ticketDao.FindByUserEmail(buyTicket.Email));

        Tickets ticketIdExist = _ticketDao.TicketIdExist(findUser, findTicket.Id);

        if (ticketIdExist != null)
        {
            throw new StudentNotFoundException($"This tickets already exists");
        }

        if (findTicket.QuantityTickets <= 0 && findTicket.QuantityTickets < buyTicket.QuantityTickets)
        {
            throw new StudentNotFoundException($"Tickets are out");
        }

        var subtraiQuantity = findTicket.QuantityTickets - buyTicket.QuantityTickets;
        findTicket.QuantityTickets = subtraiQuantity;
        findUser.TotalPrice = findTicket.Price * buyTicket.QuantityTickets;

        _messagePublisher.Publish(findTicket);

        findUser.Tickets.Add(findTicket);
        _ticketDao.SaveChanges();

        return buyTicket;
    }

    public Tickets RemoveTicketsAsync(RemoveTicketDto removeTicket)
    {
        Users findUser = _ticketDao.FindByUserEmail(removeTicket.Email);

        if (findUser == null)
        {
            throw new StudentNotFoundException($"This user does not exist");
        }

        var result = findUser.Tickets.Find(ticket => ticket.Id == removeTicket.TicketId);

        if(result == null) 
        {
            throw new StudentNotFoundException($"Tickets not exist");
        }

        findUser.Tickets.Remove(result);
        _ticketDao.SaveChanges();

        return result;
    }
}
