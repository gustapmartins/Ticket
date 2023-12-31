﻿using Microsoft.AspNetCore.Mvc;
using Ticket.DTO.Ticket;
using Ticket.Interface;
using Ticket.DTO.User;

namespace Ticket.Controles;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    ///     busca todos os usuarios
    /// </summary>
    ///     <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public List<UserViewDTO> FindAllAuth()
    {
        return _authService.FindAll();
    }

    /// <summary>
    ///     faz o login e retorna um token de acesso
    /// </summary>
    /// <param name="loginDto">Objeto com os campos necessários para criação de um filme</param>
    ///     <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> LoginAsync(LoginDTO loginDto)
    {
        var token = await _authService.Login(loginDto);
        return Ok(new { token });
    }

    /// <summary>
    ///     Faz o registro de um novo usuario
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

    /// <summary>
    ///     Faz 
    /// </summary>
    /// <param name="email">Objeto com os campos necessários para criação de um filme</param>
    ///     <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    [HttpPost("forget-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ForgetPassword([FromHeader] string email)
    {
        return Ok(await _authService.ForgetPasswordAsync(email));
    }

    /// <summary>
    ///     Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="PasswordReset">Objeto com os campos necessários para criação de um filme</param>
    ///     <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    [HttpPost("reset-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ResetPassword([FromBody] PasswordResetDto PasswordReset)
    {
        return Ok(await _authService.ResetPasswordAsync(PasswordReset));
    }
}
