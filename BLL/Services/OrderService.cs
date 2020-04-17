using AutoMapper;
using BLL.Services.Interfaces;
using BLL.Models.Order;
using DAL.Entities;
using DAL.Entities.Enums;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;

namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _database = unitOfWork;
            _mapper = mapper;
        }

        public int CreateOrder(OrderModel model)
        {
            int totalCost = 0;
            foreach(var item in model.Items)
            {
                totalCost += item.Count * _database.ProductRepository.Get(item.ProductId).Price;
            }
            Order order = new Order()
            {
                UserId = model.UserId,
                Date = DateTime.Now,
                Status = OrderStatus.NotPaid,
                TotalCost = totalCost
            };

            _database.OrderRepository.Create(order);
            _database.Save();
            
            foreach (var item in model.Items)
            {
                OrderItem orderItem = new OrderItem()
                {
                    ProductId = item.ProductId,
                    Currency = Currency.USD,
                    OrderId = order.Id,
                    Count = item.Count,
                    Amount = item.Count * _database.ProductRepository.Get(item.ProductId).Price
                };                
                _database.OrderItemRepository.Create(orderItem);
            }
            order.TotalCost = totalCost;
            _database.Save();

            return order.Id;
        }

        public IEnumerable<OrderModel> GetUserOrders(string userId)
        {
            var orders = _database.OrderRepository.Find(o => o.UserId == userId); 
            if(orders == null)
            {
                throw new Exception("Orders are null");
            }

            List<OrderModel> ordersModel = new List<OrderModel>();
            foreach(var order in orders)
            {
                OrderModel orderModel = new OrderModel()
                {
                    Id = order.Id,
                    Date = order.Date.ToString(),
                    Status = order.Status.ToString(),
                    UserId = order.UserId,
                    TotalCost= order.TotalCost,
                    Items = _mapper.Map<List<OrderItemModel>>(_database.OrderItemRepository.Find(i => i.OrderId == order.Id))
                };

                ordersModel.Add(orderModel); 
            }
            return ordersModel;
        }

        public OrderModel GetUserOrder(int orderId, string userId)
        {
            var order = _database.OrderRepository.Get(orderId);
            if (order == null)
            {
                throw new Exception("Order is null");
            }
            if(userId != order.UserId)
            {
                throw new Exception("This User does not have this order");
            }

            OrderModel orderModel = new OrderModel()
            {
                Id = order.Id,
                Date = order.Date.ToString(),
                Status = order.Status.ToString(),
                UserId = order.UserId,
                TotalCost = order.TotalCost,
                Items = _mapper.Map<List<OrderItemModel>>(_database.OrderItemRepository.Find(i => i.OrderId == order.Id))
            };             
            
            return orderModel;
        }

        public void DeleteUserOrder(int orderId, string userId)
        {
            var order = _database.OrderRepository.Get(orderId);
            if (order.UserId == userId)
            {
                var orderItems = _database.OrderItemRepository.Find(item => item.OrderId == orderId);

                foreach(var item in orderItems)
                {
                    _database.OrderItemRepository.Delete(item.Id);
                }
                _database.OrderRepository.Delete(orderId);
                _database.Save();
            }
            else
            {
                throw new Exception("This User does not have this order");
            }
        }

        public void OrderPayment(int orderId, string userId)
        {
            var order = _database.OrderRepository.Get(orderId);
            if(order.UserId == userId)
            {
                Payment payment = new Payment();
                payment.TransactionId = "0123456789";
                _database.PaymentRepository.Create(payment);
                _database.Save();

                order.PaymentId = payment.Id;
                order.Status = OrderStatus.IsDelivered;


                _database.OrderRepository.Update(order);
                _database.Save();

            }
            else
            {
                throw new Exception("This User does not have this order");
            }
        }

        public void ConfirmDelibery(int orderId, string userId)
        {
            var order = _database.OrderRepository.Get(orderId);
            if (order.UserId == userId)
            {               
                order.Status = OrderStatus.Complited;
                _database.OrderRepository.Update(order);
                _database.Save();
            }
            else
            {
                throw new Exception("This User does not have this order");
            }
        }

    }
}
