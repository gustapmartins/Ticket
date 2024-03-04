using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Ticket.ExceptionFilter;
using Ticket.DTO.Ticket;
using Ticket.Interface;
using Ticket.Model;
using Ticket.DTO.User;
using Ticket.Validation;
using System.Text;
using Ticket.Data;
using AutoMapper;
using Lib_Authentication_2FA;
using static QRCoder.PayloadGenerator;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace Ticket.Service;

public class AuthService: BaseService, IAuthService
{
    private readonly UserManager<Users> _userManager;
    private readonly TicketContext _ticketContext;
    private readonly SignInManager<Users> _signInManager;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    private readonly Authentication_2FA _authentication2FA;

    public AuthService(
        UserManager<Users> userManager,
        TicketContext ticketContext,
        IMapper mapper,
        SignInManager<Users> signInManager,
        ITokenService tokenService,
        IEmailService emailService,
        Authentication_2FA authentication2FA)
    {
        _userManager = userManager;
        _ticketContext = ticketContext;
        _mapper = mapper;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _emailService = emailService;
        _authentication2FA = authentication2FA;
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

    public async Task<ResultOperation<RegisterDTO>> RegisterAsync(RegisterDTO registerDto)
    {
        try
        {
            var existEmail = await _userManager.FindByEmailAsync(registerDto.Email);

            if (existEmail != null) 
                return CreateErrorResult<RegisterDTO>("E-mail arealdy exist");
           
            bool cpf = Lib_AttributeValidation.Validation.ValidarCpf(registerDto.Cpf!);

            if (!cpf)
                return CreateErrorResult<RegisterDTO>("Invalid CPF");

            Users user = _mapper.Map<Users>(registerDto);

            if(registerDto.TwoFactorEnabled)
            {
                var twoFactorKey = _authentication2FA.GenerateSecretKey();

                user.TwoFactorAuthKey = twoFactorKey;
                // Gere o código QR para o usuário escanear
                registerDto.qrCodeUrl = _authentication2FA.GenerateQR(registerDto.Email, twoFactorKey);
            }

            IdentityResult result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)            
                throw new InvalidOperationException($"Falha ao registrar o usuário. Erros: {string.Join(", ", result.Errors)}");

            return CreateSuccessResult(registerDto);

        }
        catch (Exception ex)
        {
            return CreateErrorResult<RegisterDTO>(ex.Message);
        }
    }

    public async Task<ResultOperation<string>> Login(LoginDTO loginDto)
    {
        try
        {

            Users user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
                throw new StudentNotFoundException($"This {loginDto.Email} not exists");

            var is2FAEnabled = await _userManager.GetTwoFactorEnabledAsync(user);

            if(is2FAEnabled)
            {
                string key = user.TwoFactorAuthKey;

                if (string.IsNullOrEmpty(key))
                    // A chave 2FA do usuário não está definida
                    throw new InvalidOperationException("Chave 2FA não encontrada para o usuário");

                bool is2FAValid = _authentication2FA.ValidateCode(loginDto.Code, key);

                if (!is2FAValid)
                    // Código 2FA inválido
                    throw new InvalidOperationException("Código 2FA inválido");
            }
            
            var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);

            if (!result.Succeeded)
                throw new StudentNotFoundException("Unauthenticated user");

            var token = _tokenService.GenerateToken(user);

            return CreateSuccessResult(token);
        }
        catch(Exception ex)
        {
            return CreateErrorResult<string>(ex.Message);
        }
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
