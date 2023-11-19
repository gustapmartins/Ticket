using Ticket.DTO.Ticket;
using Ticket.DTO.User;

namespace Ticket.Interface;

public interface IAuthService
{
    List<UserViewDTO> FindAll();

    Task<string> Login(LoginDTO loginDto);

    Task<RegisterDTO> RegisterAsync(RegisterDTO registerDto);

    Task<string> ForgetPasswordAsync(string email);

    Task<string> ResetPasswordAsync(PasswordResetDto passwordResetDto);

    string GenerateHash();
}
