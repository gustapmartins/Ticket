using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ticket.Model;

public class Show
{
    public Show() 
    {
        Id = Guid.NewGuid().ToString();
    }

    [Key]
    [Required]
    public string Id {  get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public DateTime Date { get; set; }

    public string Local { get; set; }

    public virtual Category Category { get; set; }
}
