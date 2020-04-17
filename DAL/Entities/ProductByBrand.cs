using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entities
{
    public class ProductByBrand
    {
        [Key]
        public int Id { get; set; }        
        public Brand Brand { get; set; }        
        public Product Product { get; set; }
    }
}
