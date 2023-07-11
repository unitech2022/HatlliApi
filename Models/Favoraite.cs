using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HatlliApi.Models
{
    public class Favorite
    {
        public int Id { get; set; }

        public string? UserId { get; set; }
        public int ProductId { get; set; }

        public int ProviderId { get; set; }

         public DateTime CreatedAt { get; set; }

        public Favorite()
        {

            CreatedAt = DateTime.Now;
           

        }
    }
}