
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HattliApi.Core;
using HattliApi.Models;

namespace HatlliApi.Serveries.AddressesServices
{
    public interface IRateServices :BaseInterface
    {
        
        Task<dynamic> AddRate(Rate rate);
    }
}