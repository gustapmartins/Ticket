using System.Text.Json.Serialization;

namespace Ticket.Model;

public class CartItem
{
    public CartItem()
    {
        Id = Guid.NewGuid().ToString();
    }

    public string Id { get; set; }

    public int Quantity { get; set; }

    public virtual Tickets Ticket { get; set; }
}
