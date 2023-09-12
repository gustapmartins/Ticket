using Ticket.DTO.Show;

namespace Ticket.DTO.Ticket;

public class TicketViewDto
{
    public int QuantityTickets { get; set; }

    public decimal Price { get; set; }
    
    public ShowViewDto? Show {  get; set; }
}