using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models.Order
{
    public class OrderItemModel
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Count { get; set; }

    }
}
