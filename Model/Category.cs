using System.ComponentModel.DataAnnotations;

namespace Ticket.Model;

public class Category
{
    public Category() {}

    public Category(string name, string description)
    {
        Id = Guid.NewGuid().ToString();
        Name = name; 
        Description = description;
    }

    [Key]
    [Required]
    public string Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
}
