
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HattliApi.Core;

using HatlliApi.ViewModels;
using HattliApi.Models;
using HattliApi.Models.BaseEntity;


namespace HatlliApi.Serveries.RateServices
{
    public interface IRateServices : BaseInterface
    {

        Task<dynamic> AddRate(Rate rate);

        Task<List<RateProductResponse>> GetRatesByProductId(int productId);
    }
}