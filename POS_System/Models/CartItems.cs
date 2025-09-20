﻿namespace POS_System.Models
{
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public decimal Subtotal => Product.Price * Quantity;
    }

}
