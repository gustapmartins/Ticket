using Ticket.Model;

namespace Ticket.Interface;

public interface ITokenService
{
    string GenerateToken(Users user);
}
