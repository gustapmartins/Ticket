using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Ticket.DTO.Category;
using Ticket.Interface;
using Ticket.Model;

namespace Ticket.Controles;

[ApiController]
[Route("[controller]")]
public class CategoryController: ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <summary>
    ///     Adiciona um filme ao banco de dados
    /// </summary>
    ///     <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<Category> FindAllCategory()
    {
        return _categoryService.FindAll();
    }


    [HttpGet("{id}")]
    public IActionResult FindIdCategory( int id)
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
    public CreatedAtActionResult CreateFilme([FromBody] CategoryCreateDto categoryDto)
    {
        return CreatedAtAction(nameof(FindAllCategory), _categoryService.CreateCategory(categoryDto));
    }

    /// <summary>
    ///     Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="id">Objeto com os campos necessários para criação de um filme</param>
    ///     <returns>IActionResult</returns>
    /// <response code="204">Caso inserção seja feita com sucesso</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
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
    public IActionResult UpdateCategory([FromRoute] int id, [FromBody] JsonPatchDocument<CategoryUpdateDto> categoryDto)
    {
        return Ok(_categoryService.UpdateCategory(id, categoryDto));
    }
}