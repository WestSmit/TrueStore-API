using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models.Order
{
    public class OrderModel
    {        
        public int Id { get; set; }
        public string  Status { get; set; }
        public string UserId { get; set; }
        public string Date { get; set; }
        public int TotalCost { get; set; }
        public IEnumerable<OrderItemModel> Items { get; set; }
    }
}
