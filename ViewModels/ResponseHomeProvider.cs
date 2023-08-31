using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HattliApi.Models;
using HattliApi.ViewModels;

namespace HatlliApi.ViewModels
{
    public class ResponseHomeProvider
    {

        public Address? address { get; set; }
         public List<Product>? Products { get; set; }

        public List<Category>? categories { get; set; }
        public List<OrderHome>? orders { get; set; }

        public Provider? provider { get; set; }
        public UserDetailResponse? user{ get; set; }

        public int NotiyCount { get; set; }
        
    }
}