using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HattliApi.Core;
using HattliApi.Models;
using HattliApi.Models.BaseEntity;
using HattliApi.ViewModels;

namespace HattliApi.Serveries.ProvidersService
{
    public interface IProvidersService : BaseInterface


    {
        // Task<ResponseMarketDetails> GetMarketDetails(int marketId,string userId);
Task<Provider> GitProviderByUserId(string  userId);
         Task<List<Provider>> SearchProvider(string  textSearch,int addressId);

   Task<dynamic> DetailsProvider(int providerId,string userId);
      Task<List<Provider>> GetProvidersByFieldId(int fieldId,string UserId);

        Task<Provider> UpdateProvider(Provider provider);


          Task<dynamic> ReviewProvider(string userId,string from ,int to);

            Task<dynamic> BalanceWithdrawalProvider(string userId,double mony ,int type);

    }

    
}