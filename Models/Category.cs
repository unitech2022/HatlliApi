using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HattliApi.Models
{
    public class Category
    {

        public int Id { get; set; }
        public string? Name { get; set; }

        public string? NameEng { get; set; }
        public string? ImageUrl { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public Category()
        {

            CreatedAt = DateTime.Now;
            Status=0;
            

        }
          
    }
}