using System.ComponentModel.DataAnnotations;

namespace Ticket.DTO.Category;

public class CategoryCreateDTO
{
    [Required(ErrorMessage = "O titulo do filme é obrigatório")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "O titulo do filme é obrigatório")]
    public string? Description { get; set; }
}
