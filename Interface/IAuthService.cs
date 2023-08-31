using Microsoft.AspNetCore.Identity;
using Ticket.DTO.User;
using Ticket.Model;

namespace Ticket.Interface;

public interface IAuthService
{

    List<User> FindAll();
    Task LoginAsync(LoginDTO loginDto);
    Task LogoutAsync();
    Task<RegisterDTO> RegisterAsync(RegisterDTO registerDto);
}