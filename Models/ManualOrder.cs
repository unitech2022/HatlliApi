using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HatlliApi.Models
{
    public class ManualOrder
    {
         public int Id { get; set; }
        public int ProviderId { get; set; }
        public int Status { get; set; }
        public string? UserId { get; set; }

        public double TotalCost { get; set; }
    

        public string? Desc { get; set; }

        public DateTime CreatedAt { get; set; }
      public ManualOrder()
        {

            CreatedAt = DateTime.Now.AddHours(3);
            Status=0;

        }

   
    }
}