﻿using System.IdentityModel.Tokens.Jwt;

namespace Ticket.JwtHelper;

public static class GetTokenId
{
    public static string GetClientIdFromToken(HttpContext httpContext)
    {
        var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (!string.IsNullOrEmpty(token))
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken != null)
            {
                var clientIdClaim = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "id");

                if (clientIdClaim != null)
                {
                    return clientIdClaim.Value;
                }
            }
        }

        return null;
    }
}
