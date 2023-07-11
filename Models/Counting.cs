using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HatlliApi.Models
{
    public class Counting
    {
        public int Id { get; set; }

        public int ProviderId { get; set; }

        public int CountOrders { get; set; }

        public int Status { get; set; }

        public int OrdersBestCity { get; set; }

        public DateTime FromDate { get; set; }
        public int Duration { get; set; }
// **       Duration    ** 
        // ** 0 == اسبوعي   
        // **  1 = شهرى 
        // ** 2 =  سنوى 
    }
}