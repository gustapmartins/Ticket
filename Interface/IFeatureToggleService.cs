using Ticket.DTO.Ticket;
using Ticket.Model;

namespace Ticket.Interface;

public interface IFeatureToggleService
{
    List<FeatureToggle> FindAllFeatureToggle();
    Tickets FindIdFeatureToggle(string id);
    Tickets CreateFeatureToggle(TicketCreateDto ticketDto);
    Tickets DeleteFeatureToggle(string id);
    Tickets UpdateFeatureToggle(string id, TicketUpdateDto ticketDto);
}
