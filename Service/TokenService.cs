using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ticket.Commons;
using Ticket.Interface;
using Ticket.Model;

namespace Ticket.Service;

public class TokenService: ITokenService
{   
    public string GenerateToken(Users user)
    {
        Claim[] claims = new Claim[]
        {
            new Claim("Username", user.UserName),
            new Claim("Email", user.Email),
            new Claim("Id", user.Id),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.Key));

        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken
            (
                expires: DateTime.Now.AddMinutes(10),
                claims: claims,
                signingCredentials: signingCredentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
