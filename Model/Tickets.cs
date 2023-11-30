using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ticket.Model;

public class Tickets
{
    public Tickets() 
    {
        Id = Guid.NewGuid().ToString();
    }

    [Key]
    [Required]
    public string Id { get; set; }

    [Range(1, int.MaxValue)]
    public int QuantityTickets { get; set; }

    public decimal Price { get; set; }

    public virtual Show Show { get; set; }
}
