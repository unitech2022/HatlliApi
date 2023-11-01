using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HatlliApi.Models;
using HattliApi.Core;
using HattliApi.Models;

namespace HatlliApi.Serveries.ManualOrdersServices
{
    public interface IManualOrdersServices : BaseInterface
    {

        Task<dynamic> AddOrder(ManualOrder manualOrder);

        Task<dynamic> GitOrderDetails(int orderId);


          Task<dynamic> GitOrdersByMarketId(int marketId);

       

        
       Task<dynamic> UpdateOrderStatus(int orderId,int status,int sender);

 

        Task<dynamic> GetOrdersUser(string userId);
    }
}