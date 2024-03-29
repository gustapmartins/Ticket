﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticket.Interface;
using Ticket.JwtHelper;
using Ticket.DTO.Cart;
using Ticket.Enum;

namespace Ticket.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    /// <summary>
    ///     Faz 
    /// </summary>
    ///     <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    /// <response code="404">Caso inserção não seja feita com sucesso</response>
    [HttpGet("viewCartUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult ViewCartUserId([FromHeader] StatusPayment statusPayment)
    {
        //Visualização através do JWT authenticado na aplicação
        string clientId = GetTokenId.GetClientIdFromToken(HttpContext);

        return Ok(_cartService.ViewCartPedding(clientId, statusPayment));
    }

    /// <summary>
    ///     Faz 
    /// </summary>
    ///     <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    /// <response code="404">Caso inserção não seja feita com sucesso</response>
    [HttpPost("addTicketToCart")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AddTicketToCart([FromBody] List<CreateCartDto> cartDto)
    {
        //Visualização através do JWT authenticado na aplicação
        string clientId = GetTokenId.GetClientIdFromToken(HttpContext);

        return Ok(_cartService.AddTicketToCart(cartDto, clientId));
    }

    /// <summary>
    ///     Faz 
    /// </summary>
    /// <param name="TicketId">Objeto com os campos necessários para criação</param>
    ///     <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    /// <response code="404">Caso inserção não seja feita com sucesso</response>
    [HttpPost("removeTicketToCart")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult RemoveTicketToCart([FromHeader] string CartItemId)
    {
        //Visualização através do JWT authenticado na aplicação
        string clientId = GetTokenId.GetClientIdFromToken(HttpContext);

        return Ok(_cartService.RemoveTickets(CartItemId, clientId));
    }

    /// <summary>
    ///     Faz 
    /// </summary>
    ///     <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    /// <response code="404">Caso inserção não seja feita com sucesso</response>
    [HttpPost("clearTicketsCart")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult ClearTicketsCart()
    {
        //Visualização através do JWT authenticado na aplicação
        string clientId = GetTokenId.GetClientIdFromToken(HttpContext);

        return Ok(_cartService.ClearTicketsCart(clientId));
    }


    /// <summary>
    ///     Faz 
    /// </summary>
    ///     <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    /// <response code="404">Caso inserção não seja feita com sucesso</response>
    [HttpPost("buyTicketsAsync")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public void BuyTicketsAsync()
    {
        //Visualização através do JWT authenticado na aplicação
        string clientId = GetTokenId.GetClientIdFromToken(HttpContext);

        _cartService.BuyTicketsAsync(clientId);
    }
}
