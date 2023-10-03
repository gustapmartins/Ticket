using AutoMapper;
using Ticket.DTO.Ticket;
using Ticket.Model;

namespace Ticket.Mapper;

public class TicketMapper: Profile
{
    public TicketMapper()
    {
        CreateMap<TicketCreateDto, Tickets>();
        CreateMap<Tickets, TicketViewDto>();
        CreateMap<TicketUpdateDto, Tickets>();
        CreateMap<Tickets, TicketUpdateDto>();
    }
}
