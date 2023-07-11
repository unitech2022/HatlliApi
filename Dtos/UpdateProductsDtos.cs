using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HattliApi.Dtos
{
    public class UpdateProductDto
    {
        
       
            public int ProviderId { get; set; }

        public int CategoryId { get; set; }

        public int BrandId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }
        public string? Images { get; set; }

        public string? Sizes { get; set; }

        public double? Price { get; set; }

        // public double? Discount { get; set; }

        //  public double? Status { get; set; }
        public string? Calories { get; set; }

        // public double Rate { get; set; }
    }
}