using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Ticket.DTO.Category;
using Ticket.Model;
using Ticket.Service;

namespace Ticket.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController: ControllerBase
{
    private readonly CategoryService _categoryService;

    public CategoryController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <summary>
    ///     Adiciona um filme ao banco de dados
    /// </summary>
    ///     <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    [HttpGet]
    public List<Category> FindAllFilmes()
    {
        return _categoryService.FindAll();
    }

    /// <summary>
    ///     Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="id">Objeto com os campos necessários para criação de um filme</param>
    ///     <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpGet("{id}")]
    public IActionResult FindIdFilme([FromRoute] int id)
    {
        return Ok(_categoryService.FindId(id));
    }

    /// <summary>
    ///     Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="categoryDto">Objeto com os campos necessários para criação de um filme</param>
    ///     <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public CreatedAtActionResult CreateFilme([FromBody] CategoryCreateDTO categoryDto)
    {
        return CreatedAtAction(nameof(FindAllFilmes), _categoryService.CreateCategory(categoryDto));
    }

    /// <summary>
    ///     Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="id">Objeto com os campos necessários para criação de um filme</param>
    ///     <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult DeleteCategory(int id)
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
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdateCategory([FromRoute] int id, [FromBody] JsonPatchDocument<CategoryUpdateDTO> categoryDto)
    {
        return Ok(_categoryService.UpdateCategory(id, categoryDto));
    }
}