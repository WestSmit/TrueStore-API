using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Entities
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public string TransactionId { get; set; }
    }
}
