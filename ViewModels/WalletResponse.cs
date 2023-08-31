using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HatlliApi.Models;
using HattliApi.Models;
using HattliApi.ViewModels;

namespace HatlliApi.ViewModels
{
    public class WalletResponse
    {
        public OrderWallet? wallet { get; set; }

        public Provider? provider { get; set; }

        public UserDetailResponse? userDetail { get; set; }
    }


}