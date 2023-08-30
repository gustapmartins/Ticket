using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Ticket.Data;
using Ticket.DTO.User;
using Ticket.ExceptionFilter;
using Ticket.Interface;
using Ticket.Model;

namespace Ticket.Service;

public class AuthService: IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IMapper _mapper;
    public AuthService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public List<User> FindAll()
    {
            try
            {
                var find = _userManager.Users.ToList();

                if (find.Count == 0)
                {
                    throw new StudentNotFoundException("The list is empty");
                }
                return find;
            }
            catch (Exception ex)
            {
                throw new StudentNotFoundException("Error in the request", ex);
            }
    }

    public Task LoginAsync(LoginDTO loginDto)
    {
        throw new Exception();
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

        User user = _mapper.Map<User>(registerDto);

        IdentityResult result = await _userManager.CreateAsync(user, registerDto.Password);

        if(!result.Succeeded) throw new StudentNotFoundException("Failed to register the user");

        return registerDto;
    }
}
