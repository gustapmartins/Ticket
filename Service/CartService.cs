﻿using Microsoft.AspNetCore.Identity;
using Ticket.Repository.Dao;
using Ticket.Validation;
using Ticket.DTO.Ticket;
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

    public CartAddDto AddTicketToCart(CartAddDto CartDto)
    {
        Users user = _userManager.Users.FirstOrDefault(user => user.Id == CartDto.UserId);
        //se o carrinho do usuario não existir ele cria um novo
        if (user != null)
        {
            Cart cart = _cartDao.FindId(user.Id);

            if(cart == null) {

                cart = new Cart
                {
                    Users = user,
                    Tickets = new List<Tickets>()
                };
            }

            foreach (var ticketId in CartDto.TicketsId)
            {
                Tickets ticket = _ticketDao.FindId(ticketId);

                if (ticket != null && !cart.Tickets.Contains(ticket))
                {
                    cart.Tickets.Add(ticket);
                }
            }
                
            _cartDao.Add(cart);
        }

        return CartDto;
    }

    public void ClearCart(string userId)
    {
        Cart cart = _cartDao.FindId(userId);

        if (cart != null)
        {
            cart.Tickets.Clear();
            _cartDao.Add(cart);
        }
    }

    public Tickets RemoveTicketsAsync(RemoveTicketDto removeTicket)
    {
        throw new NotImplementedException();
    }
}
