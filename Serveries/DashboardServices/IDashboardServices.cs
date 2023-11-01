using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HatlliApi.ViewModels;
using HattliApi.Models;
using HattliApi.Models.BaseEntity;

namespace HatlliApi.Serveries.DashboardServices
{
    public interface IDashboardServices
    {
          Task<DashboardHomeResponse> GetHomeDashboard(string userId);

           Task<object> UpdateStatusProvider(int providerId,int status);

          Task<BaseResponse> GetProviders(int page,string textSearch);
           Task<List<Category>> GetCategories();

          Task<BaseResponse> GetProducts(int page,string textSearch);


           Task<BaseResponse> GetUsers(int page,string textSearch);

              Task<BaseResponse> GetWallets(int page);

           Task<object> UpdateStatusWallet(int walletId,int status);   
            Task<object> UpdateStatusProduct(int productId,int status);   


              Task<object> BlockUser(string userId,int status);   

              Task<BaseResponse> GetOrders(int page);

                Task<dynamic> PaymentProvider(string userId , double mony,int type );

             
    }
}