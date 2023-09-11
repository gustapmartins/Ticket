using System.ComponentModel.DataAnnotations;

namespace Ticket.DTO.Show;

public class ShowCreateDto
{
    [Required(ErrorMessage = "Name é obrigatório")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Description é obrigatório")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Date é obrigatório")]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "Local é obrigatório")]
    public string? Local { get; set; }

    [Required(ErrorMessage = "price é obrigatório")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Category é obrigatório")]
    public string? CategoryName { get; set; }
}
