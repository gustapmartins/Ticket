using System.ComponentModel.DataAnnotations;

namespace Ticket.DTO.Show;

public class ShowCreateDto
{
    [Required(ErrorMessage = "Name é obrigatório")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Description é obrigatório")]
    public string Description { get; set; }

    [Required(ErrorMessage = "CEP é obrigatório")]
    public string CEP { get; set; }

    [Required(ErrorMessage = "CategoryName é obrigatório")]
    public string CategoryName { get; set; }
}
