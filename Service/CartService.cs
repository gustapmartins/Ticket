using Microsoft.AspNetCore.Identity;
using Ticket.Repository.Dao;
using Ticket.Validation;
using Ticket.Interface;
using Ticket.DTO.Cart;
using Ticket.Model;
using AutoMapper;
using Ticket.DTO.Ticket;
using Ticket.ExceptionFilter;

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

    public Cart ViewCartUserId(string id)
    {
        return HandleErrorAsync(() => _cartDao.FindCartUser(id));
    }

    public Cart AddTicketToCart(CartAddDto CartDto)
    {
        Users user = _userManager.Users.FirstOrDefault(user => user.Id == CartDto.UserId)!;
        //se o carrinho do usuario não existir ele cria um novo

        if (user != null)
        {
            Cart cart = _cartDao.FindId(user.Id);

            if (cart == null)
            {
                cart = new Cart
                {
                    Users = user,
                    TicketsCart = new List<Tickets>()
                };
            }

            foreach (var ticketId in CartDto.TicketsId)
            {
                Tickets ticket = _ticketDao.FindId(ticketId);

                if (ticket != null && !cart.TicketsCart.Contains(ticket))
                {
                    cart.TicketsCart.Add(ticket);
                }
            }

            _cartDao.Add(cart);
            return cart;
        }

        return null;
    }

    public Cart RemoveTickets(CartRemoveDto cartRemoveDto)
    {
        Cart cart = HandleErrorAsync(() => _cartDao.FindId(cartRemoveDto.CartId));

        var ticketId = cart.TicketsCart.Find(ticket => ticket.Id == cartRemoveDto.TicketId);

        cart.TicketsCart.Remove(ticketId);
        _cartDao.SaveChanges();

        return cart;
    }

    public void ClearCart(string userId)
    {
        Cart cart = _cartDao.FindId(userId);

        if (cart != null)
        {
            cart.TicketsCart.Clear();
            _cartDao.Add(cart);
        }
    }

    public BuyTicketDto BuyTicketsAsync(BuyTicketDto buyTicket)
    {
        //esse metodo só funciona com o projeto worker, que é um processador de fila do rabbitMQ

        Tickets findTicket = HandleErrorAsync(() => _ticketDao.FindId(buyTicket.TicketId));

        Users findUser = HandleErrorAsync(() => _ticketDao.FindByUserEmail(buyTicket.Email));

        //Tickets ticketIdExist = _ticketDao.TicketIdExist(findUser, findTicket.Id);

        //if (ticketIdExist != null)
        //{
        //    throw new StudentNotFoundException($"This tickets already exists");
        //}

        if (findTicket.QuantityTickets <= 0 && findTicket.QuantityTickets < buyTicket.QuantityTickets)
        {
            throw new StudentNotFoundException($"Tickets are out");
        }

        findTicket.QuantityTickets = findTicket.QuantityTickets - buyTicket.QuantityTickets;
        findUser.TotalPrice = findTicket.Price * buyTicket.QuantityTickets;

        _messagePublisher.Publish(buyTicket);

        //findUser.Tickets.Add(findTicket);
        //_ticketDao.SaveChanges();

        return buyTicket;
    }
}
