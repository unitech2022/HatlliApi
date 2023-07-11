using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HatlliApi.ViewModels;
using HattliApi.Dtos;
using HattliApi.Models;


namespace HatlliApi.Serveries.CartsService
{
    public interface ICartsService
    {
          Task<ResponseCart> GetCarts(string UserId);


        // Task<BaseResponse> GitCartsByCategoryId(string UserId,int categoryId,int page );

         Task<Cart> AddCart(Cart Cart);

         Task<Cart> GitCartById(int CartId);


         Task<Cart> DeleteCart(int CartId);

        Task<Cart> UpdateCart(UpdateCartDto cart ,int id);

          Task<Cart> UpdateQuantityCart(int Quantity ,int id);


         bool SaveChanges(); 
    }
}