using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HattliApi.Models
{

    // id userId restaurantId totlalCost Tax 
    // deliveryCost driverId productsCost 
    // status notes CreatedAt
    public class Order
    {
        public int Id { get; set; }
        public int ProviderId { get; set; }

        public int Status { get; set; }
        public string? UserId { get; set; }

        public double TotalCost { get; set; }
        public int payment { get; set; }   // *** => 0 cash &&  1 payment

        public int Type { get; set; }  // *** => 0 normal (cart) &&  1 manual without cart

        public double ProductsCost { get; set; }

        public string? DriverId { get; set; }

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; }

        public Order()
        {

            CreatedAt = DateTime.Now.AddHours(3);
            Status=0;

        }

       
    }
}