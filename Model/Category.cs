using System.ComponentModel.DataAnnotations;

namespace Ticket.Model;

public class Category
{
    [Key]
    [Required]
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
}
