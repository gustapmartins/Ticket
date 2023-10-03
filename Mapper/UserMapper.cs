using AutoMapper;
using Ticket.DTO.Category;
using Ticket.DTO.Ticket;
using Ticket.Model;

namespace Ticket.Mapper;

public class UserMapper: Profile
{
    public UserMapper()
    {
        CreateMap<RegisterDTO, Users>();
    }
}
