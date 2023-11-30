using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Ticket.ExceptionFilter;
using Ticket.DTO.Ticket;
using Ticket.Interface;
using Ticket.Model;
using System.Text;
using Ticket.Data;
using AutoMapper;
using Ticket.DTO.User;
using Ticket.Validation;

namespace Ticket.Service;

public class AuthService: TicketBase, IAuthService
{
    private readonly UserManager<Users> _userManager;
    private readonly TicketContext _ticketContext;
    private readonly SignInManager<Users> _signInManager;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;

    public AuthService(
        UserManager<Users> userManager,
        TicketContext ticketContext,
        IMapper mapper, 
        SignInManager<Users> signInManager, 
        ITokenService tokenService,
        IEmailService emailService
    )
    {
        _userManager = userManager;
        _ticketContext = ticketContext;
        _mapper = mapper;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _emailService = emailService;
    }

    public List<UserViewDTO> FindAll()
    {
        try
        {
            var find = _userManager.Users.ToList();

            if (find.Count == 0) throw new StudentNotFoundException("The list is empty");

            var userView = _mapper.Map<List<UserViewDTO>>(find);

            return userView;
        }
        catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }

    public async Task<RegisterDTO> RegisterAsync(RegisterDTO registerDto)
    {
        var existEmail = await _userManager.FindByEmailAsync(registerDto.Email);

        if (existEmail != null)
        {
            throw new StudentNotFoundException("This email already exists");
        }

        if (registerDto.YearsOld <= 16)
        {
            throw new StudentNotFoundException("This email already exists");
        }

        Users user = _mapper.Map<Users>(registerDto);
        
        IdentityResult result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded) throw new StudentNotFoundException("Failed to register the user");

        return registerDto;
    }

    public async Task<string> Login(LoginDTO loginDto)
    {

        var email = await _userManager.FindByEmailAsync(loginDto.Email);

        if (email == null)
        {
            throw new StudentNotFoundException($"This {loginDto.Email} already exists");
        }

        var result = await _signInManager.PasswordSignInAsync(email, loginDto.Password, false, false);

        if (!result.Succeeded)
        {
            throw new StudentNotFoundException("Unauthenticated user");
        }

        var user = _signInManager
            .UserManager
            .Users
            .FirstOrDefault(user => user.NormalizedEmail == loginDto.Email!.ToUpper());

        if(user == null)
        {
            throw new StudentNotFoundException("User is null");
        }

        var token = _tokenService.GenerateToken(user);

        return token;
    }

    public async Task<string> ForgetPasswordAsync(string email)
    {
        try
        {
            var findEmail = await _userManager.FindByEmailAsync(email);

            if (findEmail == null) 
            {
                throw new StudentNotFoundException($"This {email} is not valid");
            }

            var token = GenerateHash();

            var PasswordReset = new PasswordReset
            {
                Token = token,
                Email = email,
                TokenExpire = DateTime.UtcNow.AddMinutes(10),
            };

            _ticketContext.PasswordResets.Add(PasswordReset);
            _ticketContext.SaveChanges();

            _emailService.SendMail(
                  email,
                  "Redefinição da sua senha",
                  $"Verifique sua conta, com essa token: {token}"
               );

            return "Um e-mail de redefinição de senha foi enviado para o seu endereço de e-mail";
        }
        catch (DbUpdateException ex)
        {
            throw new StudentNotFoundException($"This is user not exist {ex}");
        }
    }

    public async Task<string> ResetPasswordAsync(PasswordResetDto passwordResetDto)
    {
        var passwordReset = _ticketContext.PasswordResets.FirstOrDefault(reset => 
            reset.Token == passwordResetDto.Token);

        if(passwordReset == null)
        {
            throw new StudentNotFoundException($"This is not exist token: {passwordResetDto.Token}");
        }

        var user = await _userManager.FindByEmailAsync(passwordReset.Email);

        if(user == null)
        {
            throw new StudentNotFoundException($"This is user not exist");
        }

        if(passwordResetDto.Password != passwordResetDto.ConfirmPassword)
        {
            throw new StudentNotFoundException($"The password must be the same");
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        var result = await _userManager.ResetPasswordAsync(user, token, passwordResetDto.Password);

        if (!result.Succeeded)
        {
            throw new StudentNotFoundException($"Failed to reset the password: {string.Join(", ", result.Errors)}");
        }

        _ticketContext.PasswordResets.Remove(passwordReset);
        await _ticketContext.SaveChangesAsync();

        return "Senha redefinida com sucesso";
    }

    public string GenerateHash()
    {
        string randomValue = Guid.NewGuid().ToString();
        SHA256 sha256 = SHA256.Create();
        byte[] bytes = Encoding.UTF8.GetBytes(randomValue);
        byte[] hashBytes = sha256.ComputeHash(bytes);
        string hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        return hash;
    }
}
