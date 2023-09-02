using Ticket.DTO.User;
using Ticket.Model;

namespace Ticket.Interface;

public interface IAuthService
{
    List<Users> FindAll();

    Task Login(LoginDTO loginDto);

    Task<RegisterDTO> RegisterAsync(RegisterDTO registerDto);
}
