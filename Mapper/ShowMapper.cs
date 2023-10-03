using AutoMapper;
using Ticket.DTO.Show;
using Ticket.Model;

namespace Ticket.Mapper;

public class ShowMapper: Profile
{
    public ShowMapper()
    {
        CreateMap<ShowCreateDto, Show>();
        CreateMap<Show, ShowViewDto>()
            .ForMember(showDto => showDto.Category, opt => opt.MapFrom(show => show.Category));
        CreateMap<ShowUpdateDto, Show>();
        CreateMap<Show, ShowUpdateDto>();
    }
}
