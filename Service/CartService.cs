using Microsoft.AspNetCore.Identity;
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
        Users user = HandleErrorAsync(() => _userManager.Users.FirstOrDefault(user => user.Id == clientId)!);

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

            if (ticket != null && CreateCartDto.Quantity <= ticket.QuantityTickets)
            {
                ticket.QuantityTickets -= CreateCartDto.Quantity;
                cart.TotalPrice += ticket.Price * CreateCartDto.Quantity;

                var cartMapper = _mapper.Map<CartItem>(CreateCartDto);
                cartMapper.Ticket = ticket;

                cart.CartList.Add(cartMapper);
            }
        }

        _cartDao.Add(cart);

        return cart;
    }

    public CartViewDto RemoveTickets(string cartId, string clientId)
    {
        Cart cart = HandleErrorAsync(() => _cartDao.FindId(clientId));

        CartItem cartItemId = cart.CartList.FirstOrDefault(ticket => ticket.Id == cartId);

        if (cartItemId != null)
        {
            // Recupere a quantidade do ticket no carrinho
            Tickets ticket = _ticketDao.FindId(cartItemId.Ticket.Id);

            if (ticket != null)
            {

                cart.TotalPrice -= ticket.Price * cartItemId.Quantity;

                ticket.QuantityTickets += cartItemId.Quantity;

                cart.CartList.Remove(cartItemId);

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
        Cart cart = HandleErrorAsync(() => _cartDao.FindId(clientId));

        _messagePublisher.Publish(cart);

        cart.CartList.Clear();
        _cartDao.Add(cart);

        return "Compra Finalizada";
    }
}
