using System.ComponentModel.DataAnnotations;
using Ticket.Model;

namespace Ticket.Enum;

public class Roles
{
    [Key]
    [Required]
    public int Id { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public virtual Show Show { get; set; }
}
