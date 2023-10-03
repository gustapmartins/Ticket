using AutoMapper;
using Ticket.DTO.Ticket;
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
