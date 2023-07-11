using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HatlliApi.Models;
using HattliApi.ViewModels;

namespace HattliApi.Models
{
    public class ResponseHomeUser
    {
      
    public Address? address { get; set; }
       public  List<Provider>? providers{ get; set; }

       public List<Favorite>? favorites { get; set; }
   public List<Cart>? carts { get; set; }
    public List<OrderHome>? orders { get; set; }
    public UserDetailResponse? user { get; set; }
   public List<Category>? categories { get; set; }
     
  
  public int NotiyCount { get; set; }
      
    }


    public class OrderHome
    {
      

 public Order? order { get; set; }

   public Address? address { get; set; }
    public string? name { get; set; }

 

  public string? imageUrl { get; set; }
      

      
    }
}



