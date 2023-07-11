using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HattliApi.Core;

namespace HatlliApi.Serveries.AddressesServices
{
    public interface IAddressesServices :BaseInterface
    {
        
        Task<dynamic> DefaultAddress(int typeId,string userId);
    }
}