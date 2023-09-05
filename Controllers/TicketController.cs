using Microsoft.AspNetCore.Mvc;
using Ticket.Interface;

namespace Ticket.Controllers;

[ApiController]
[Route("[controller]")]
public class TicketController: ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }
}
