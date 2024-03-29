﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticket.DTO.Category;
using Ticket.Interface;

namespace Ticket.Controles;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class CategoryController: ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly ICachingService _cachingService;

    public CategoryController(ICategoryService categoryService, ICachingService cachingService)
    {
        _categoryService = categoryService;
        _cachingService = cachingService;
    }

    /// <summary>
    ///     Adiciona um filme ao banco de dados
    /// </summary>
    ///     <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult FindAllCategory()
    {
        return Ok(_categoryService.FindAllCategory());
    }

    /// <summary>
    ///     Consultar categoria pelo id
    /// </summary>
    /// <param name="id">Objeto com os campos necessários para criação de um filme</param>
    ///     <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    [HttpGet("{id}")]
    public async Task<IActionResult> FindIdCategory(string id)
    {
        string chaveRedis = $"Category:FindIdCategory:{id}";

        return Ok(await _cachingService.StringGetSet(chaveRedis, () => 
             _categoryService.FindIdCategory(id)
        ));
    }

    /// <summary>
    ///     Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="categoryDto">Objeto com os campos necessários para criação de um filme</param>
    ///     <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost, Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public CreatedAtActionResult CreateFilme([FromBody] CategoryCreateDto categoryDto)
    {
        return CreatedAtAction(nameof(FindAllCategory), _categoryService.CreateCategory(categoryDto));
    }

    /// <summary>
    ///     Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="id">Objeto com os campos necessários para criação de um filme</param>
    ///     <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    [HttpDelete("{id}"), Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult DeleteCategory([FromRoute] string id)
    {
        return Ok(_categoryService.DeleteCategory(id));
    }


    /// <summary>
    ///     Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="categoryDto">Objeto com os campos necessários para criação de um filme</param>
    /// <param name="id">Objeto com os campos necessários para criação de um filme</param>
    ///     <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPatch("{id}"), Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdateCategory([FromRoute] string id, [FromBody] CategoryUpdateDto categoryDto)
    {
        return Ok(_categoryService.UpdateCategory(id, categoryDto));
    }
}