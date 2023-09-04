using System.ComponentModel.DataAnnotations;

namespace Ticket.Model;

public class Tickets
{
    [Key]
    [Required]
    public int Id { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public virtual Show Show { get; set; }
}
