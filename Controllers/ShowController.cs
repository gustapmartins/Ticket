﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticket.Interface;
using Ticket.DTO.Show;
using Microsoft.AspNetCore.Hosting;

namespace Ticket.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ShowController : ControllerBase
{
    private readonly IShowService _showService;
    private readonly ICachingService _cachingService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ShowController(IShowService showService, ICachingService cacheService, IWebHostEnvironment webHostEnvironment)
    {
        _showService = showService;
        _cachingService = cacheService;
        _webHostEnvironment = webHostEnvironment;
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
        return Ok(_showService.FindAllShow());
    }

    /// <summary>
    ///     Adiciona um filme ao banco de dados
    /// <param name="id">Objeto com os campos necessários para criação de um filme</param> 
    /// </summary>
    ///     <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    [HttpGet("{id}"), Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> FindIdShow(string id)
    {
        string chaveRedis = $"Show:FindIdShow:{id}";

        return Ok(await _cachingService.StringGetSet(chaveRedis, () =>
                _showService.FindIdShow(id)
         ));
    }

    /// <summary>
    ///     Busca uma imagem no banco de dados
    /// </summary>
    /// <param name="showDto">Objeto com os campos necessários para criação de um filme</param>
    ///     <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    [HttpGet("GetImage/{fileName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetImage([FromRoute] string fileName)
    {
       var fileBytes = _showService.GetImagem(fileName);

       return File(fileBytes, "image/png");
    }


    /// <summary>
    ///    Adicione um show ao banco de daods
    /// </summary>
    /// <param name="showDto">Objeto com os campos necessários para criação de um filme</param>
    ///     <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost, Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<CreatedAtActionResult> CreateShow([FromForm] ShowCreateDto showDto)
    {
        return CreatedAtAction(nameof(FindAllShow),await _showService.CreateShow(showDto));
    }


    /// <summary>
    ///     Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="Id">Objeto com os campos necessários para criação de um filme</param>
    ///     <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    [HttpDelete("{id}"), Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult DeleteShow(string Id)
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
    [HttpPatch("{id}"), Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdateShow([FromRoute] string id, [FromBody] ShowUpdateDto showDto)
    {
        return Ok(_showService.UpdateShow(id, showDto));
    }
}
