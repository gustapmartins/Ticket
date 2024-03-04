using Ticket.DTO.Ticket;
using Ticket.DTO.User;
using Ticket.Model;

namespace Ticket.Interface;

public interface IAuthService
{
    List<UserViewDTO> FindAll();

    Task<ResultOperation<string>> Login(LoginDTO loginDto);

    Task<ResultOperation<RegisterDTO>> RegisterAsync(RegisterDTO registerDto);

    Task<string> ForgetPasswordAsync(string email);

    Task<string> ResetPasswordAsync(PasswordResetDto passwordResetDto);

    string GenerateHash();
}
