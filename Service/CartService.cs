using Microsoft.AspNetCore.Identity;
using Ticket.ExceptionFilter;
using Ticket.Repository.Dao;
using Ticket.Validation;
using Ticket.Interface;
using Ticket.DTO.Cart;
using Ticket.Model;
using AutoMapper;

namespace Ticket.Service;

public class CartService : TicketBase, ICartService
{
    private readonly IMapper _mapper;
    private readonly ICartDao _cartDao;
    private readonly ITicketDao _ticketDao;
    private readonly IMessagePublisher _messagePublisher;
    private readonly UserManager<Users> _userManager;

    public CartService(IMapper mapper, ITicketDao ticketDao, ICartDao cartDao, IMessagePublisher messagePublisher, UserManager<Users> userManager)
    {
        _mapper = mapper;
        _cartDao = cartDao;
        _ticketDao = ticketDao;
        _messagePublisher = messagePublisher;
        _userManager = userManager;
    }

    public Cart ViewCartUserId(string clientId)
    {
        return HandleErrorAsync(() => _cartDao.FindCartUser(clientId));
    }

    public Cart AddTicketToCart(List<CreateCartDto> ticketQuantityDto, string clientId)
    {
        Users user = _userManager.Users.FirstOrDefault(user => user.Id == clientId)!;

        if (user == null)
        {
            throw new StudentNotFoundException("This user does not exist");
        }

        Cart cart = _cartDao.FindCartUser(user.Id);

        if (cart == null)
        {
            cart = new Cart
            {
                Users = user,
                TicketsCart = new List<Tickets>()
            };
        }

        foreach (var ticketQuantitId in ticketQuantityDto)
        {
            Tickets ticket = _ticketDao.FindId(ticketQuantitId.TicketId);

            if (ticket != null)
            {
                if(ticketQuantitId.Quantity <= ticket.QuantityTickets)
                {
                    ticket.QuantityTickets -= ticketQuantitId.Quantity;

                    cart.TicketsCart.Add(ticket);
                }
            }
        }

        _cartDao.Add(cart);
        return cart;
    }

    public Cart RemoveTickets(string TicketId, string clientId)
    {
        Cart cart = HandleErrorAsync(() => _cartDao.FindId(clientId));

        var ticketId = cart.TicketsCart.Find(ticket => ticket.Id == TicketId);

        cart.TicketsCart.Remove(ticketId);
        _cartDao.SaveChanges();

        return cart;
    }

    public Cart ClearTicketsCart(string clientId)
    {
        Cart cart = _cartDao.FindId(clientId);

        if (cart != null)
        {
            cart.TicketsCart.Clear();
            _cartDao.Add(cart);
        }

        return cart;
    }

    public string BuyTicketsAsync(string clientId)
    {
        //esse metodo só funciona com o projeto worker, que é um processador de fila do rabbitMQ
        Cart findCart = HandleErrorAsync(() => _cartDao.FindId(clientId));

        _messagePublisher.Publish(findCart);

        findCart.TicketsCart.Clear();
        _cartDao.Add(findCart);

        return "Compra Finalizada";
    }
}
