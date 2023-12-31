﻿using System.ComponentModel.DataAnnotations;
namespace Ticket.Model;

public class Cart
{

    [Key]
    [Required]
    public string Id { get; set; }

    public virtual List<CartItem> CartList { get; set; }

    public int Quantity { get; set; }

    public decimal TotalPrice { get; set; }

    public virtual Users Users { get; set; }
}
