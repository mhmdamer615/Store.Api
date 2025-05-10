using Shared.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IOrderService
    {

        Task<OrderResult> GetOrderbyIdAsync(Guid id);
        Task<IEnumerable<OrderResult>> GetOrderbyEmailAsync(string email);
        Task<OrderResult> CreateOrderAsync(OrderRequest orderRequest, string buyerEmail);
        Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodResultsAsync();


    }
}
