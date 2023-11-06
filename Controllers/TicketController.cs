using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticket.DTO.Ticket;
using Ticket.Interface;

namespace Ticket.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TicketController : ControllerBase
{

    private readonly ITicketService _ticketService;
    private readonly ICachingService _cachingService;

    public TicketController(ITicketService ticketService, ICachingService cacheService)
    {
        _ticketService = ticketService;
        _cachingService = cacheService;
    }

    /// <summary>
    ///     Adiciona um filme ao banco de dados
    /// </summary>
    ///     <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult FindAll()
    {
        return Ok(_ticketService.FindAllTicket());
    }

    /// <summary>
    ///     Adiciona um filme ao banco de dados
    /// <param name="id">Objeto com os campos necessários para criação de um filme</param> 
    /// </summary>
    ///     <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> FindIdTicket(int id)
    {
        string chaveRedis = $"Ticket:FindIdTicket:{id}";

        return Ok(await _cachingService.StringGetSet(chaveRedis, async () => 
            await _ticketService.FindIdTicket(id)
        ));
    }

    /// <summary>
    ///     Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="ticketDto">Objeto com os campos necessários para criação de um filme</param>
    ///     <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost, Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public CreatedAtActionResult CreateTicket([FromBody] TicketCreateDto ticketDto)
    {
        return CreatedAtAction(nameof(FindAll), _ticketService.CreateTicket(ticketDto));
    }

    /// <summary>
    ///     Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="buyTicket">Objeto com os campos necessários para criação de um filme</param>
    ///     <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    [HttpPost("buyTicket"), Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult BuyTickets([FromBody] BuyTicketDto buyTicket)
    {
        return Ok(_ticketService.BuyTicketsAsync(buyTicket));
    }

    /// <summary>
    ///     Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="id">Objeto com os campos necessários para criação de um filme</param>
    ///     <returns>IActionResult</returns>
    /// <response code="204">Caso inserção seja feita com sucesso</response>
    [HttpDelete("{id}"), Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteTicket([FromRoute] int id)
    {
        return Ok(_ticketService.DeleteTicket(id));
    }

    /// <summary>
    ///     Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="removeTicket">Objeto com os campos necessários para criação de um filme</param>
    ///     <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    [HttpDelete("RemoveTicket"), Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult RemoveTicketsAsync([FromBody] RemoveTicketDto removeTicket)
    {
        return Ok(_ticketService.RemoveTicketsAsync(removeTicket));
    }

    /// <summary>
    ///     Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="ticketDto">Objeto com os campos necessários para criação de um filme</param>
    /// <param name="id">Objeto com os campos necessários para criação de um filme</param>
    ///     <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    [HttpPatch("{id}"), Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdateTicket([FromRoute] int id, [FromBody] TicketUpdateDto ticketDto)
    {
        return Ok(_ticketService.UpdateTicket(id, ticketDto));
    } 
}