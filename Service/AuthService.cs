using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Ticket.Data;
using Ticket.DTO.User;
using Ticket.ExceptionFilter;
using Ticket.Interface;
using Ticket.Model;

namespace Ticket.Service;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IMapper _mapper;
    private readonly RoleManager<IdentityUser> _roleManager;

    public AuthService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityUser> roleMnager)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleMnager;
    }

    public List<User> FindAll()
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

    public async Task LoginAsync(LoginDTO loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user == null) throw new StudentNotFoundException("Incorrect email and password");

        var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, false);

        if (!result.Succeeded) throw new StudentNotFoundException("unauthenticated user");
    }

    public Task LogoutAsync()
    {
        throw new Exception();
    }

    public async Task<RegisterDTO> RegisterAsync(RegisterDTO registerDto)
    {
        var existEmail = await _userManager.FindByEmailAsync(registerDto.Email);

        if (existEmail != null)
        {
            throw new StudentNotFoundException("This email already exists");
        }

        if(registerDto.YearsOld <= 16)
        {
            throw new StudentNotFoundException("This email already exists");
        }

        User user = new User{
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = registerDto.Username,
            Email = registerDto.Email,
            YearsOld = registerDto.YearsOld,
            Cpf = registerDto.Cpf,
            EmailConfirmed = true
        };

        IdentityResult result = await _userManager.CreateAsync(user, registerDto.Password);

        if(!result.Succeeded) throw new StudentNotFoundException("Failed to register the user");

        if (!await _roleManager.RoleExistsAsync(registerDto.Role))
            await _roleManager.CreateAsync(new IdentityUser(registerDto.Role));

        if(await _roleManager.RoleExistsAsync(registerDto.Role))
        {
            await _userManager.AddToRoleAsync(user, registerDto.Role);
        }

        return registerDto;
    }
}
