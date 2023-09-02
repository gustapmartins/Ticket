using Microsoft.AspNetCore.Mvc;
using Ticket.DTO.User;
using Ticket.Interface;
using Ticket.Model;
using Ticket.Service;

namespace Ticket.Controllers;

[ApiController]
[Route("[Controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    ///     Adiciona um filme ao banco de dados
    /// </summary>
    ///     <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public List<Users> FindAllFilmes()
    {
        return _authService.FindAll();
    }

    /// <summary>
    ///     Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="loginDto">Objeto com os campos necessários para criação de um filme</param>
    ///     <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginDTO loginDto)
    {
        await _authService.Login(loginDto);
        return Ok("Logado");
    }

    /// <summary>
    ///     Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="registerDto">Objeto com os campos necessários para criação de um filme</param>
    ///     <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Register(RegisterDTO registerDto)
    {
        return Ok(await _authService.RegisterAsync(registerDto));
    }
}
