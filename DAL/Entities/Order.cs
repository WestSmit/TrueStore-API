using DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public OrderStatus Status { get; set; }        
        public string UserId { get; set; }
        public DateTime Date { get; set; }      
        public int TotalCost { get; set; }
        public int PaymentId { get; set; }
    }
}
