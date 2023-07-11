using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HatlliApi.ViewModels;
using HattliApi.Core;
using HattliApi.Models;
using HattliApi.ViewModels;

namespace HattliApi.Serveries.HomeService
{
    public interface IHomeService 
    {
          Task<ResponseHomeUser> GetHomeUserData(string UserId);

          

           Task<ResponseHomeProvider> GetHomeDataProvider(string UserId);
    }
}