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
    private readonly IMapper _mapper;
    private RoleManager<IdentityUser> _roleManager;
    public AuthService(UserManager<Users> userManager, IMapper mapper, SignInManager<Users> signInManager = null, RoleManager<IdentityUser> roleManager = null)
    {
        _userManager = userManager;
        _mapper = mapper;
        _signInManager = signInManager;
        _roleManager = roleManager;
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

        var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, false);

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

        Users user = new Users
        {
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = registerDto.Username,
            Email = registerDto.Email,
            YearsOld = registerDto.YearsOld,
            Cpf = registerDto.Cpf,
            EmailConfirmed = true
        };

        IdentityResult result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded) throw new StudentNotFoundException("Failed to register the user");

        if (!await _roleManager.RoleExistsAsync(registerDto.Role))
            await _roleManager.CreateAsync(new IdentityUser(registerDto.Role));

        if (await _roleManager.RoleExistsAsync(registerDto.Role))
        {
            await _userManager.AddToRoleAsync(user, registerDto.Role);
        }

        return registerDto;
    }
}
