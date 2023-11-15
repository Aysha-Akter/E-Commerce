
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Models.OrderEntities
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string BuyerID { get; set; }

        [DisplayFormat(DataFormatString = "{0:F}")]
        public DateTime OrderedAt { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        public string Email { get; set; }

        [RegularExpression(@"^?([0−9]3)?([0-9]{3})?([0−9]3)?([0-9]{8})$", ErrorMessage = "It's not a valid phone number")]
        [Display(Name = "Mobile number")]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:N}")]
        [Display(Name = "Shipping Charge")]
        public decimal ShippingCharge { get; set; }

        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:N}")]
        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        [DisplayFormat(DataFormatString = "{0:F}")]
        public DateTime? CompletedAt { get; set; }

        public Payment Payment { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public void UpdateStatus(OrderStatus status)
        {
            Status = status;
        }
    }

    public enum OrderStatus
    {
        Pending,
        Accepted,
        Rejected
    }
}