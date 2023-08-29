using System.ComponentModel.DataAnnotations;

namespace Ticket.DTO.Category;

public class CategoryUpdateDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
}
