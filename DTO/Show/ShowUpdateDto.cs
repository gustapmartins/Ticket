using System.ComponentModel.DataAnnotations;

namespace Ticket.DTO.Show;

public class ShowUpdateDto
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime Date { get; set; }

    public string? Local { get; set; }

    public string? Category { get; set; }
}
