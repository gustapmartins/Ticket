using AutoMapper;
using Ticket.DTO.Category;
using Ticket.DTO.Show;
using Ticket.DTO.Ticket;
using Ticket.Model;

namespace Ticket.Mapper;

public class TicketMapper: Profile
{
    public TicketMapper()
    {
        CreateMap<TicketCreateDto, Tickets>();
        CreateMap<TicketUpdateDto, Tickets>();
        CreateMap<Tickets, TicketViewDto>()
        .ForMember(tickets => tickets.ShowId, opt => opt.MapFrom(tickets => tickets.ShowId));
    }
}
