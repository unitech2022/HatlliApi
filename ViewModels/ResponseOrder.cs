using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HattliApi.Models;
using HattliApi.ViewModels;

namespace HatlliApi.ViewModels
{
    public class ResponseOrder
    {

          public Order? order { get; set; }
         public  List<OrderDetails>? Products { get; set; }

       public Provider? provider { get; set; }
         public Address? address { get; set; }

         public UserDetailResponse? userDetail { get; set; }
    }
}

public class OrderDetails{

    public OrderItem? Order { get; set; }
    public Product? Product { get; set; }

   
}

