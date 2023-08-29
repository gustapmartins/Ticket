using Ticket.DTO.User;

namespace Ticket.Interface;

public interface IAuthService
{
    Task LoginAsync(LoginDTO loginDto);
    Task LogoutAsync();
    Task RegisterAsync(RegisterDTO loginDto);
}
