using Ticket.DTO.Category;

namespace Ticket.DTO.Show;

public class ShowViewDto
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime Date { get; set; }

    public string? Local { get; set; }

    public decimal Price { get; set; }

    public CategoryViewDto? Category { get; set; }
}
