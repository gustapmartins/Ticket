using Microsoft.AspNetCore.Identity;
using Ticket.Repository.Dao;
using Ticket.Validation;
using Ticket.Interface;
using Ticket.DTO.Cart;
using Ticket.Model;
using Ticket.Enum;
using AutoMapper;

namespace Ticket.Service;

public class CartService : BaseService, ICartService
{
    private readonly IMapper _mapper;
    private readonly ICartDao _cartDao;
    private readonly ITicketDao _ticketDao;
    private readonly UserManager<Users> _userManager;
    private readonly IMessagePublisher _messagePublisher;

    public CartService(IMapper mapper, ITicketDao ticketDao, ICartDao cartDao, IMessagePublisher messagePublisher, UserManager<Users> userManager)
    {
        _mapper = mapper;
        _cartDao = cartDao;
        _ticketDao = ticketDao;
        _messagePublisher = messagePublisher;
        _userManager = userManager;
    }

    public ResultOperation<CartViewDto> ViewCartPedding(string clientId, StatusPayment statusPayment)
    {
        try
        {
            Carts cart = HandleError(() => _cartDao.FindCartPedding(clientId, statusPayment));

            var cartViewMapper = _mapper.Map<CartViewDto>(cart);

            return CreateSuccessResult(cartViewMapper);
        }
        catch(Exception ex)
        {
            return CreateErrorResult<CartViewDto>(ex.Message);
        }
    }

    public ResultOperation<Carts> AddTicketToCart(List<CreateCartDto> CreateCartsDto, string clientId)
    {
        try
        {
            Users user = HandleError(() => _userManager.Users.FirstOrDefault(user => user.Id == clientId)!);

            Carts cart = _cartDao.FindId(user.Id);

            if (cart == null)
            {
                cart = new Carts
                {
                    Id = Guid.NewGuid().ToString(),
                    Users = user,
                    CartList = new List<CartItem>(),
                    TotalPrice = 0,
                };
            }

            AddCartItems(CreateCartsDto, cart);

            _cartDao.Add(cart);

            return CreateSuccessResult(cart);
        }
        catch(Exception ex)
        {
            return CreateErrorResult<Carts>(ex.Message);
        }
    }

    private void AddCartItems(List<CreateCartDto> CreateCartsDto, Carts cart)
    {
        foreach (var CreateCartDto in CreateCartsDto)
        {
            Tickets ticket = _ticketDao.FindId(CreateCartDto.TicketId);

            if (ticket != null && CreateCartDto.Quantity <= ticket.QuantityTickets)
            {
                var existCartItem = cart.CartList.FirstOrDefault(c => c.Ticket.Id == ticket.Id);

                if (existCartItem != null && existCartItem.statusPayment.Equals(StatusPayment.Pedding))
                {
                    existCartItem.Quantity += CreateCartDto.Quantity;
                    cart.TotalPrice += ticket.Price * CreateCartDto.Quantity;
                }
                else
                {
                    ticket.QuantityTickets -= CreateCartDto.Quantity;
                    cart.TotalPrice += ticket.Price * CreateCartDto.Quantity;

                    var cartMapper = _mapper.Map<CartItem>(CreateCartDto);
                    cartMapper.Ticket = ticket;

                    cart.CartList.Add(cartMapper);
                }
            }
        }
    }

    public ResultOperation<CartViewDto> RemoveTickets(string cartItemId, string clientId)
    {
        try
        {
            Carts cart = HandleError(() => _cartDao.FindId(clientId));

            CartItem cartItem = HandleError(() => cart.CartList.FirstOrDefault(ticket => ticket.Id == cartItemId))!;

            if (cartItem != null)
            {
                // Recupere a quantidade do ticket no carrinho
                Tickets ticket = _ticketDao.FindId(cartItem.Ticket.Id);

                if (ticket != null)
                {

                    cart.TotalPrice -= ticket.Price * cartItem.Quantity;
                    ticket.QuantityTickets += cartItem.Quantity;
                    cart.CartList.Remove(cartItem);
                }

                _cartDao.SaveChanges();
            }

            var cartViewMapper = _mapper.Map<CartViewDto>(cart);

            return CreateSuccessResult(cartViewMapper);
        }catch (Exception ex)
        {
            return CreateErrorResult<CartViewDto>(ex.Message);
        }
    }

    public ResultOperation<CartViewDto> ClearTicketsCart(string clientId)
    {
        try
        {
            Carts cart = HandleError(() => _cartDao.FindId(clientId));

            var cartViewMapper = _mapper.Map<CartViewDto>(cart);

            _cartDao.Remove(cart);

            return CreateSuccessResult(cartViewMapper);
        }
        catch(Exception ex)
        {
            return CreateErrorResult<CartViewDto>(ex.Message);
        }
    }

    public void BuyTicketsAsync(string clientId)
    {
        //esse metodo só funciona com o projeto worker, que é um processador de fila do rabbitMQ
        try
        {
            Carts cart = HandleError(() => 
                _cartDao.FindCartPedding(clientId, StatusPayment.Pedding));

            _messagePublisher.Publish(cart);
        }
        catch(Exception ex)
        {
            CreateErrorResult<CartViewDto>(ex.Message);
        }
    }
}
