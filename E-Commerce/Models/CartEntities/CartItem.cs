using E_Commerce.Models.ProductEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Models.CartEntities
{
    public class CartItem
    {
        public int ID { get; private set; }
        public int Quantity { get; private set; }

        public int CartID { get; private set; }
        public Cart Cart { get; private set; }

        public int ProductID { get; private set; }
        public Product Product { get; private set; }

        private CartItem()
        {
            // required by EF
        }

        public CartItem(int productId, int quantity)
        {
            ProductID = productId;
            Quantity = quantity;
        }

        public void AddQuantity(int quantity)
        {
            Quantity += quantity;
        }

        public void SetNewQuantity(int quantity)
        {
            Quantity = quantity;
        }

        public decimal Total()
        {
           return (Quantity * Product?.Price ?? 0);
        }
    }
}
