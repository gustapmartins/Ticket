using System.ComponentModel.DataAnnotations;

namespace Ticket.Model;

public class Cart
{

    public Cart()
    {
        Id = Guid.NewGuid().ToString();
    }

    [Key]
    [Required]
    public string Id { get; set; }

    public virtual Users Users { get; set; }

    public virtual List<Tickets> Tickets { get; set; }
}
