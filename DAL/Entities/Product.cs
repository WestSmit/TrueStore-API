using DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Characteristic { get; set; }
        public int Price { get; set; }
        public string Status { get; set; }
        public Currency Currency { get; set; }

        [DefaultValue("false")]
        public bool IsRemoved { get; set; }
        public string PreviewImg { get; set; }
        public string OtherImgs { get; set; }
        
    }
}
