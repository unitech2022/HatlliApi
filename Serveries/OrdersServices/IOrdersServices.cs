using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HatlliApi.Models;
using HattliApi.Core;
using HattliApi.Models;

namespace HatlliApi.Serveries.OrdersServices
{
    public interface IOrdersServices : BaseInterface
    {

        Task<dynamic> AddOrder(string userId,int payment,string nots);

        Task<dynamic> AddManualOrder(ManualOrder manualOrder);

        Task<dynamic> GitOrderDetails(int orderId);


          Task<dynamic> GitOrdersByMarketId(int marketId);

          Task<dynamic> GitOrdersByDriverId(string driverId, int AddressId);

        
       Task<dynamic> UpdateOrderStatus(int orderId,int status,int sender);

       Task<dynamic> ConfirmManualOrderStatus(int orderId,double price);

         Task<dynamic> PaymentOrder(int orderId,int payment);

        Task<dynamic> GetOrdersUser(string userId);

          Task<User> GetUser(string userId);
    }
}