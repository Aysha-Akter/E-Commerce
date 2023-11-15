using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_Commerce.Models.CartEntities
{
    public class Cart
    {
        public int ID { get; private set; }

        public string BuyerId { get; private set; }

        public int? DeliveryMethod { get; private set; }

        public ICollection<CartItem> Items { get; set; }

        private Cart()
        {
            // required by EF
        }

        public Cart(string buyerId)
        {
            BuyerId = buyerId;
        }

        public void AddDeliveryMethod(int deliveryMethod)
        {
            DeliveryMethod = deliveryMethod;
        }
    }
}
