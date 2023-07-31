using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HatlliApi.Models
{
    public class OrderWallet
    {
        public int Id { get; set; }

        public string? UserId { get; set; }
        public double Mony { get; set; }
         
          public int Status { get; set; }
          public DateTime CreatedAt { get; set; }

        public OrderWallet()
        {

            CreatedAt = DateTime.Now;
            Status = 0 ;
          

        }


    }
}