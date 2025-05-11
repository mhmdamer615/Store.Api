using AutoMapper;
using Domain.Contract;
using Domain.Entities;
using Domain.Entities.Identity;
using Domain.Entities.OrderEntities;
using Domain.Exceptions;
using Services.Abstraction;
using Services.Specification;
using Shared.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService(IUnitOfWork unitOfWork, IMapper mapper, IBasketRepository basketRepository) : IOrderService
    {
        public async Task<OrderResult> CreateOrderAsync(OrderRequest orderRequest, string buyerEmail)
        {
            var address = mapper.Map<Domain.Entities.OrderEntities.Address>(orderRequest.shippingAddress);

            var basket = await basketRepository.GetBasketAsync(orderRequest.BasketId);

            if (basket is null)
                throw new BasketNotFoundException(orderRequest.BasketId);
            var orderRepo = unitOfWork.GetRepository<Order, Guid>();
            var existingOrderSpecs = new OrderWithPaymentIntentIdSpecification(basket.PaymentIntentId);
            var existingOrder = await orderRepo.GetAsync(existingOrderSpecs);
            if (existingOrder is not null)
                orderRepo.Delete(existingOrder);


            var orderItems = new List<OrderItem>();
            var productRepo = unitOfWork.GetRepository<Product, int>();

            foreach (var item in basket.Items)
            {
                var product = await productRepo.GetAsync(item.Id);
                if (product is null)
                    throw new ProductNotFoundException(item.Id);

                var productInOrderItem = new ProductInOrderItem(product.Id, product.Name, product.PictureUrl);

                var orderItem = new OrderItem(productInOrderItem, item.Quantity, item.Price);
                orderItems.Add(orderItem);


            }
            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAsync(orderRequest.DeliverymethodId);

            if (deliveryMethod is null)
                throw new DeliveryMethodNotFoundException(orderRequest.DeliverymethodId);

            var subtotal = orderItems.Sum(item => item.Price * item.Quantity);
            var order = new Order(buyerEmail, address, orderItems, deliveryMethod, subtotal);
            await orderRepo.AddAsync(order);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<OrderResult>(order);




        }

        public async Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodResultsAsync()
        {
            var deliveryMethods = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return mapper.Map<IEnumerable<DeliveryMethodResult>>(deliveryMethods);
        }

        

        public async Task<OrderResult> GetOrderbyIdAsync(Guid id)
        {
            var orderSpecs = new OrderWithIncludeSpecification(id);
            var order = await unitOfWork.GetRepository<Order, Guid>().GetAsync(orderSpecs);
            if (order is null)
                throw new OrderNotFoundException(id);
            return mapper.Map<OrderResult>(order);
        }

        

        public async Task<IEnumerable<OrderResult>> GetOrderbyEmailAsync(string email)
        {
            var orderSpecs = new OrderWithIncludeSpecification(email);
            var orders = await unitOfWork.GetRepository<Order, Guid>().GetAllAsync(orderSpecs);
            return mapper.Map<IEnumerable<OrderResult>>(orders);

        }
    }
}
