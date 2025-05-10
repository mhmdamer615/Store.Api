using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderDtos
{
    public record OrderResult
    {
        public Guid Id { get; set; }
        public string buyerEmail { get; set; }
        public AddressDto ShippingAddress { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
        public string PaymentStatus { get; set; } 
        public string DeliveryMethod { get; set; }
        public int? DeliveryMethodId { set; get; }
        public DateTimeOffset OrderData { get; set; } 
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }

    }
}
