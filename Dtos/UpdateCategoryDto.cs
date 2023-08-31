using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HattliApi.Dtos
{
    public class UpdateCategoryDto
    {
         public string? Name { get; set; }
          public string? NameEng { get; set; }
          public string? ImageUrl { get; set; }

        public int Status { get; set; }
    }
}