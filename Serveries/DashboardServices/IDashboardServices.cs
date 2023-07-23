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

          Task<BaseResponse> GetProducts(int page,string textSearch);

           Task<BaseResponse> GetUsers(int page,string textSearch);

            Task<object> UpdateStatusProduct(int productId,int status);   

              Task<BaseResponse> GetOrders(int page);

             
    }
}