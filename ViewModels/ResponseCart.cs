using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HattliApi.Models;

namespace HatlliApi.ViewModels
{
    public class ResponseCart
    {
       public  List<CartDetails>? Carts { get; set; }


       public double CountProducts { get; set; }
       public double TotalCost { get; set; }
    //    public double DeliveryCost { get; set; }
    //    public double Tax { get; set; }

    //    public CouponResponse? CouponDetails { get; set; }
     
    }
}

public class CartDetails{

    public Cart? Cart { get; set; }
    public Product? Product { get; set; }

   
}

public class CouponResponse{
   
   public string? Status { get; set; }

   public string? Message { get; set; }

   public Coupon? coupon { get; set; }

}