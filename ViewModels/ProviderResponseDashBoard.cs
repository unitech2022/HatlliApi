using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HattliApi.Models;
using HattliApi.ViewModels;

namespace HatlliApi.ViewModels
{
    public class ProviderResponseDashBoard
    {
        public Provider? provider { get; set; }

        public UserDetailResponse? userDetail { get; set; }
    }
}