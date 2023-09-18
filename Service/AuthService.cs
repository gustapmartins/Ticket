using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Ticket.DTO.User;
using Ticket.ExceptionFilter;
using Ticket.Interface;
using Ticket.Model;

namespace Ticket.Service;

public class AuthService: IAuthService
{
    private readonly UserManager<Users> _userManager;
    private readonly SignInManager<Users> _signInManager;
    private readonly IMapper _mapper;
    private TokenService _tokenService;

    public AuthService(UserManager<Users> userManager, IMapper mapper, SignInManager<Users> signInManager, TokenService tokenService)
    {
        _userManager = userManager;
        _mapper = mapper;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public List<Users> FindAll()
    {
        try
        {
            var find = _userManager.Users.ToList();

            if (find.Count == 0) throw new StudentNotFoundException("The list is empty");

            return find;
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
            throw new StudentNotFoundException("This email already exists");
        }

        var result = await _signInManager.PasswordSignInAsync(email, loginDto.Password, false, false);

        if (!result.Succeeded)
        {
            throw new StudentNotFoundException("Unauthenticated user");
        }

        var user = _signInManager
            .UserManager
            .Users
            .FirstOrDefault(user => user.NormalizedEmail == loginDto.Email.ToUpper());

        if(user == null)
        {
            throw new StudentNotFoundException("User is null");
        }

        var token = _tokenService.GenerateToken(user);

        return token;
    }
}
