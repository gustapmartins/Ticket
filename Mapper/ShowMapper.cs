using AutoMapper;
using Ticket.DTO.Show;
using Ticket.DTO.User;
using Ticket.Model;

namespace Ticket.Mapper;

public class ShowMapper: Profile
{
    public ShowMapper()
    {
        CreateMap<ShowCreateDto, Show>();
        CreateMap<ShowUpdateDto, Show>();
        CreateMap<Show, ShowViewDto>()
         .ForMember(showDto => showDto.Category, opt => opt.MapFrom(showDto => showDto.Category))
         .ForMember(showDto => showDto.Tickets, opt => opt.MapFrom(showDto => showDto.Tickets));
    }
}
