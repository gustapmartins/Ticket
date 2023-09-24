using AutoMapper;
using Ticket.DTO.User;
using Ticket.Model;

namespace Ticket.Mapper;

public class PasswordResetMapper: Profile
{
    public PasswordResetMapper()
    {
        CreateMap<PasswordResetDto, PasswordReset>();
        CreateMap<PasswordReset, PasswordResetDto>();
    }
}
