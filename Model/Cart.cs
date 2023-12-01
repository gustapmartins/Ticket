using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ticket.Model;

public class Cart
{

    public Cart()
    {
        Id = Guid.NewGuid().ToString();
        TicketsCart = new List<Tickets>();
    }

    [Key]
    [Required]
    public string Id { get; set; }

    public virtual List<Tickets> TicketsCart { get; set; }

    public virtual Users Users { get; set; }
}
