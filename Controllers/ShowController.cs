using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Ticket.DTO.Show;
using Ticket.Interface;
using Ticket.Model;
using Ticket.Service;

namespace Ticket.Controllers;

[ApiController]
[Route("[controller]")]
public class ShowController : ControllerBase
{
    private readonly IShowService _showService;

    public ShowController(IShowService showService)
    {
        _showService = showService;
    }

    /// <summary>
    ///     Adiciona um filme ao banco de dados
    /// </summary>
    ///     <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult FindAllShow()
    {
        return Ok(_showService.FindAll());
    }

    /// <summary>
    ///     Adiciona um filme ao banco de dados
    /// <param name="id">Objeto com os campos necessários para criação de um filme</param> 
    /// </summary>
    ///     <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult FindId(int id)
    {
        return Ok(_showService.FindId(id));
    }

    /// <summary>
    ///     Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="showDto">Objeto com os campos necessários para criação de um filme</param>
    ///     <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public CreatedAtActionResult CreateShow([FromBody] ShowCreateDto showDto)
    {
        return CreatedAtAction(nameof(FindAllShow), _showService.CreateShow(showDto));
    }

    /// <summary>
    ///     Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="Id">Objeto com os campos necessários para criação de um filme</param>
    ///     <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult DeleteShow(int Id)
    {
        return Ok(_showService.DeleteShow(Id));
    }

    /// <summary>
    ///     Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="showDto">Objeto com os campos necessários para criação de um filme</param>
    /// <param name="id">Objeto com os campos necessários para criação de um filme</param>
    ///     <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdateShow([FromRoute] int id, [FromBody] JsonPatchDocument<ShowUpdateDto> showDto)
    {
        return Ok(_showService.UpdateShow(id, showDto));
    }
}
