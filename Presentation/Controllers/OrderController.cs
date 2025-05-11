using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Authorize]
    public class OrderController(IServiceManager serviceManager) : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<OrderResult>> Create(OrderRequest orderRequest)
        {
            var email = User.FindFirst(ClaimTypes.Email).Value;
            var order = await serviceManager.orderService.CreateOrderAsync(orderRequest, email);
            return Ok(order);


        }

        [HttpGet]
        public async Task<ActionResult<OrderResult>> GetOrderById(Guid id)
        => Ok(await serviceManager.orderService.GetOrderbyIdAsync(id));

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResult>>> GetOrdersByEmail()
        {
            var email = User.FindFirst(ClaimTypes.Email).Value;
            var orders = await serviceManager.orderService.GetOrderbyEmailAsync(email);
            return Ok(orders);
        }

        public async Task<ActionResult<IEnumerable<DeliveryMethodResult>>> GetDeliveryMethods()
        => Ok(await serviceManager.orderService.GetDeliveryMethodResultsAsync());
    }
}
