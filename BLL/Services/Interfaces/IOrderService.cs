using BLL.Models.Order;
using System.Collections.Generic;

namespace BLL.Services.Interfaces
{
    public interface IOrderService
    {
        int CreateOrder(OrderModel model);
        IEnumerable<OrderModel> GetUserOrders(string userId);
        OrderModel GetUserOrder(int orderId, string userId);
        void DeleteUserOrder(int orderId, string userId);
        void OrderPayment(int orderId, string userId);
        void ConfirmDelibery(int orderId, string userId);

    }
}
