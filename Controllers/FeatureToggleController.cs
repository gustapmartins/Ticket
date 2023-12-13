using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticket.DTO.FeatureToggle;
using Ticket.Interface;

namespace Ticket.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class FeatureToggleController : ControllerBase
{
    private readonly IFeatureToggleService _featureToggleService;
    private readonly ICachingService _cachingService;

    public FeatureToggleController(IFeatureToggleService featureToggleService, ICachingService cachingService)
    {
        _featureToggleService = featureToggleService;
        _cachingService = cachingService;
    }

    /// <summary>
    ///     Busca todas as feature toggles da aplicação
    /// </summary>
    ///     <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    /// <response code="404">Caso inserção seja feita sem sucesso</response>
    [HttpGet("consultarFeatureToggle")]
    public async Task<IActionResult> FindAllFeatureToggle()
    {
        string chaveRedis = $"FeatureToggleService:FindAllFeatureToggle";

        return Ok(await _cachingService.StringGetSet(chaveRedis, () =>
             _featureToggleService.FindAllFeatureToggle()
        ));
    }

    /// <summary>
    ///     Buscar apenas feature toggle pelo nome
    /// </summary>
    ///     <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    [HttpGet("consultarFeatureToggleIdOrName")]
    public async Task<IActionResult> FindNameFeatureToggle([FromHeader] string idOrName)
    {
        string chaveRedis = $"FeatureToggleService:FindIdFeatureToggle:{idOrName}";

        return Ok(await _cachingService.StringGetSet(chaveRedis, () =>
             _featureToggleService.FindIdFeatureToggle(idOrName)
        ));
    }

    /// <summary>
    ///     Adiciona uma Feature Toggle ao banco de dados
    /// </summary>
    /// <param name="categoryDto">Objeto com os campos necessários para criação de um filme</param>
    ///     <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost, Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public CreatedAtActionResult CreateFeatureToggle([FromBody] FeatureToggleCreateDto createFeatureToggleDto)
    {
        return CreatedAtAction(nameof(FindAllFeatureToggle), _featureToggleService.CreateFeatureToggle(createFeatureToggleDto));
    }

    /// <summary>
    ///     Delete uma FeatureToggleService a partir do nome dela
    /// </summary>
    /// <param name="nameFeatureToggle">Objeto com os campos necessários para criação de uma FeatureToggleService</param>
    ///     <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    [HttpDelete("{id}"), Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult DeleteCategory([FromRoute] string Id)
    {
        return Ok(_featureToggleService.DeleteFeatureToggle(Id));
    }


    /// <summary>
    ///     Edita uma FeatureToggleService a partir do id dela
    /// </summary>
    /// <param name="featureToggleCreateAndUpdateDto">Objeto com os campos necessários para atualizar uma FeatureToggleService</param>
    /// <param name="id">Objeto com os campos necessários para criação de um filme</param>
    ///     <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPatch("{id}"), Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdateCategory([FromRoute] string id, [FromBody] FeatureToggleUpdateDto featureToggleCreateAndUpdateDto)
    {
        return Ok(_featureToggleService.UpdateFeatureToggle(id, featureToggleCreateAndUpdateDto));
    }
}
