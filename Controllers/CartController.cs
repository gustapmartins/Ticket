using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticket.DTO.Cart;
using Ticket.Interface;

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
    /// <param name="CartDto">Objeto com os campos necessários para criação</param>
    ///     <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    /// <response code="404">Caso inserção não seja feita com sucesso</response>
    [HttpPost("addTicketToCart")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult AddTicketToCart([FromBody] CartAddDto CartAddDto)
    {
        return Ok(_cartService.AddTicketToCart(CartAddDto));
    }

    /// <summary>
    ///     Faz 
    /// </summary>
    /// <param name="CartDto">Objeto com os campos necessários para criação</param>
    ///     <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    /// <response code="404">Caso inserção não seja feita com sucesso</response>
    [HttpPost("removeTicketToCart")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult RemoveTicketToCart([FromBody] CartRemoveDto CartRemoveDto)
    {
        return Ok(_cartService.RemoveTickets(CartRemoveDto));
    }
}
