using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HattliApi.Models;
using HattliApi.ViewModels;
namespace HatlliApi.ViewModels
{
    public class RateProductResponse
    {
        public Rate? rate { get; set; }

        public UserDetailResponse? user { get; set; }
    }
}