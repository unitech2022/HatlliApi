using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HattliApi.Models
{
    public class Provider
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public string? Title { get; set; }


        public string? UserId { get; set; }

        public string? Email { get; set; }
        public string? About { get; set; }

        public string? LogoCompany { get; set; }

        public string? ImagePassport { get; set; }

        public string? NameAdministratorCompany { get; set; }

        public string? AddressName { get; set; }

          public string? IBan { get; set; }

        public string? NameBunk { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }

        public double Rate { get; set; }

        public int Status { get; set; }

        public double discount { get; set; }

         public double Area { get; set; }

        public double Distance { get; set; }

        public double Wallet { get; set; }

        public DateTime CreatedAt { get; set; }
        public Provider()
        {

            CreatedAt = DateTime.Now;
            Distance = 0.0;
            discount = 0.0;
            Lat = 0.0;
            Lng = 0.0;
            Rate = 0.0;
            Wallet = 0.0;

        }

    }
}