using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HattliApi.Models
{

    // id name userId description page pageId createdAt
    public class Alert
    {

          public int Id { get; set; }
          public string? UserId { get; set; }

          public string? title { get; set; }
          public string? Description { get; set; }
          public string? Type { get; set; }
          public int PageId { get; set; }

         public bool Viewed { get; set; }

        public DateTime CreatedAt { get; set; }
      
        public Alert()
        {

            CreatedAt = DateTime.Now;
            Viewed=false;

        }
        
    }
}