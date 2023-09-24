﻿using Ticket.DTO.User;
using Ticket.Model;

namespace Ticket.Interface;

public interface IAuthService
{
    List<Users> FindAll();

    Task<string> Login(LoginDTO loginDto);

    Task<RegisterDTO> RegisterAsync(RegisterDTO registerDto);

    Task<string> ForgetPasswordAsync(string email);

    Task<string> ResetPassword(PasswordResetDto passwordResetDto);

    string GenerateHash();
}
