    using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Ticket.DTO.User;
using Ticket.ExceptionFilter;
using Ticket.Interface;
using Ticket.Model;

namespace Ticket.Service;

public class AuthService: IAuthService
{
    private UserManager<Users> _userManager;
    private SignInManager<Users> _signInManager;
    private IMapper _mapper;
    public AuthService(UserManager<Users> userManager, IMapper mapper, SignInManager<Users> signInManager)
    {
        _userManager = userManager;
        _mapper = mapper;
        _signInManager = signInManager;
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

    public async Task Login(LoginDTO loginDto)
    {

        var result = await _signInManager.PasswordSignInAsync(loginDto.Username, loginDto.Password, false, false);

        if (!result.Succeeded)
        {
            throw new StudentNotFoundException("Unauthenticated user");
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
}
