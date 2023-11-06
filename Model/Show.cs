using System.ComponentModel.DataAnnotations;

namespace Ticket.Model;

public class Show
{
    [Key]
    [Required]
    public int Id {  get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public DateTime Date { get; set; }
    
    public string Local { get; set; }

    public virtual Category? Category { get; set; }
}
