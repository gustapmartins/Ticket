using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
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
    private readonly UserManager<Users> _userManager;

    public TicketService(ITicketDao ticketDao, IMapper mapper, UserManager<Users> userManager)
    {
        _mapper = mapper;
        _ticketDao = ticketDao;
        _userManager = userManager;
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

            //decimal totalPrice = show.Price * ticketDto.QuantityTickets;

            var tickets = new Tickets
            {
                Price = ticketDto.Price,
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

    public async Task<BuyTicketDto> BuyTicketsAsync(BuyTicketDto buyTicket)
    {
        //busca o ticket que o usuario está informando o id
        Tickets findTicket = _ticketDao.FindId(buyTicket.TicketId);

        if (findTicket == null)
        {
            throw new StudentNotFoundException($"This ticket does not exist");
        }

        Users findUser = await _userManager.Users.FirstAsync(user => user.Email == buyTicket.Email);

        if (findUser == null)
        {
            throw new StudentNotFoundException($"This user does not exist");
        }

        if (findTicket.QuantityTickets <= 0 && findTicket.QuantityTickets < buyTicket.QuantityTickets)
        {
            throw new StudentNotFoundException($"Tickets are out");
        }

        var subtraiQuantity = findTicket.QuantityTickets - buyTicket.QuantityTickets;
        findUser.TotalPrice = findTicket.Price * buyTicket.QuantityTickets;
        findTicket.QuantityTickets = subtraiQuantity;

        //verifica se os tickets do usuarios já existem
        var ticketIdExist = findUser.Tickets!.Find(ticketId => ticketId.Id == findTicket.Id);

        if (ticketIdExist != null)
        {
            throw new StudentNotFoundException($"This tickets already exists");
        }

        findUser.Tickets.Add(findTicket);
        _ticketDao.SaveChanges();

        return buyTicket;
    }

    public async Task<Tickets> RemoveTicketsAsync(RemoveTicketDto removeTicket)
    {
        Tickets findTicket = _ticketDao.FindId(removeTicket.TicketId);

        if (findTicket == null)
        {
            throw new StudentNotFoundException($"This ticket does not exist");
        }

        Users findUser = await _userManager.Users.FirstAsync(user => user.Email == removeTicket.Email);

        if (findUser == null)
        {
            throw new StudentNotFoundException($"This user does not exist");
        }

        findUser.Tickets!.Remove(findTicket);
        _ticketDao.SaveChanges();

        return findTicket;
    }
}
