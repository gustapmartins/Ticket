using System.ComponentModel.DataAnnotations;

namespace Ticket.DTO.Cart;

public class CartRemoveDto
{
    [Required(ErrorMessage = "O CartId do carrinho é obrigatório")]
    public string CartId { get; set; }

    [Required(ErrorMessage = "O ticketId do usuario é obrigatório")]
    public string TicketId { get; set; }
}
