using AutoMapper;
using Ticket.DTO.Category;
using Ticket.DTO.User;
using Ticket.Model;

namespace Ticket.Mapper;

public class UserMapper: Profile
{
    public UserMapper()
    {
        CreateMap<RegisterDTO, User>();
    }
}
