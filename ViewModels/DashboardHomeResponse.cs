using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HattliApi.Models;
using HattliApi.ViewModels;

namespace HatlliApi.ViewModels
{
    public class DashboardHomeResponse
    {
        public int Providers { get; set; }

        public int Products { get; set; }

        public int Orders { get; set; }

        public int Users { get; set; }

        public List<Provider>? LastProviders { get; set; }


        public UserDetailResponse? AdminDetails { get; set; }
    }
}