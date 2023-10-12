using System.ComponentModel.DataAnnotations;

namespace Ticket.DTO.Category;

public class CategoryCreateDto
{
    [Required(ErrorMessage = "O Name do filme é obrigatório")]
    public string Name { get; set; }

    [Required(ErrorMessage = "A Description do filme é obrigatório")]
    public string Description { get; set; }
}
