using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace HattliApi.Models
{
    public class Product
    {

        public int Id { get; set; }

        public int ProviderId { get; set; }

        public int CategoryId { get; set; }

        public string? BrandId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }
        public string? Images { get; set; }

        public string? Sizes { get; set; }

        public double? Price { get; set; }

        public double? Discount { get; set; }

         public double? Status { get; set; }
        public string? Calories { get; set; }

        public double Rate { get; set; }
      
        public DateTime CreatedAt { get; set; }

        public Product()
        {

            CreatedAt = DateTime.Now;
            Status=0.0;
            Rate=0.0;
            Discount=0.0;


        }
    }
}