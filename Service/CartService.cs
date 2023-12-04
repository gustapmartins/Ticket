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

    public CartViewDto ViewCartUserId(string clientId)
    {
        var cart = HandleErrorAsync(() => _cartDao.FindCartUser(clientId));

        var cartViewMapper = _mapper.Map<CartViewDto>(cart);

        return cartViewMapper;
    }

    public Cart AddTicketToCart(List<CreateCartDto> CreateCartsDto, string clientId)
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
                Id = Guid.NewGuid().ToString(),
                Users = user,
                CartList = new List<CartItem>(),
                TotalPrice = 0,
            };
        }

        foreach (var CreateCartDto in CreateCartsDto)
        {
            Tickets ticket = _ticketDao.FindId(CreateCartDto.TicketId);

            if (ticket != null)
            {
                if(CreateCartDto.Quantity <= ticket.QuantityTickets)
                {
                    ticket.QuantityTickets -= CreateCartDto.Quantity;
                    cart.TotalPrice = ticket.Price * ticket.QuantityTickets;

                    var cartMapper = _mapper.Map<CartItem>(CreateCartDto);

                    cart.CartList.Add(cartMapper);
                }
            }
        }

        _cartDao.Add(cart);

        return cart;
    }

    public CartViewDto RemoveTickets(string cartListId, string clientId)
    {
        Cart cart = HandleErrorAsync(() => _cartDao.FindId(clientId));

        var ticketId = cart.CartList.FirstOrDefault(ticket => ticket.Id == cartListId);

        if (ticketId != null)
        {
            cart.CartList.Remove(ticketId);

            // Recupere a quantidade do ticket no carrinho
            Tickets ticket = _ticketDao.FindId(ticketId.Ticket.Id);

            if (ticket != null)
            {
                ticket.QuantityTickets += cart.Quantity;
            }

            _cartDao.SaveChanges();
        }

        var cartViewMapper = _mapper.Map<CartViewDto>(cart);

        return cartViewMapper;
    }

    public Cart ClearTicketsCart(string clientId)
    {
        Cart cart = _cartDao.FindId(clientId);

        if (cart != null)
        {
            cart.CartList.Clear();
            _cartDao.Add(cart);
        }

        return cart;
    }

    public string BuyTicketsAsync(string clientId)
    {
        //esse metodo só funciona com o projeto worker, que é um processador de fila do rabbitMQ
        Cart findCart = HandleErrorAsync(() => _cartDao.FindId(clientId));

        _messagePublisher.Publish(findCart);

        findCart.CartList.Clear();
        _cartDao.Add(findCart);

        return "Compra Finalizada";
    }
}
