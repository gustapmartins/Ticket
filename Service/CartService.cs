﻿using Microsoft.AspNetCore.Identity;
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

    public Carts AddTicketToCart(List<CreateCartDto> CreateCartsDto, string clientId)
    {
        Users user = HandleErrorAsync(() => _userManager.Users.FirstOrDefault(user => user.Id == clientId)!);

        Carts cart = _cartDao.FindCartUser(user.Id);

        if (cart == null)
        {
            cart = new Carts
            {
                Id = Guid.NewGuid().ToString(),
                Users = user,
                CartList = new List<CartItem>(),
                TotalPrice = 0,
                statusPayment = Enum.StatusPayment.pedding
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

    public CartViewDto RemoveTickets(string cartItemId, string clientId)
    {
        Carts cart = HandleErrorAsync(() => _cartDao.FindId(clientId));

        CartItem cartItem = cart.CartList.FirstOrDefault(ticket => ticket.Id == cartItemId);

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

        return cartViewMapper;
    }

    public Carts ClearTicketsCart(string clientId)
    {
        Carts cart = _cartDao.FindId(clientId);

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
        Carts cart = HandleErrorAsync(() => _cartDao.FindId(clientId));

        if(cart.statusPayment == Enum.StatusPayment.pedding)
        {
            _messagePublisher.Publish(cart);

            return "Compra Efetuada";
        }

        return "Não foi possivel fazer a compra";
    }
}
