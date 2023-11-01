using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HatlliApi.Models;
using HattliApi.Models;
using HattliApi.ViewModels;

namespace HatlliApi.ViewModels
{
    public class ResponseOrder
    {

        public Order? order { get; set; }
        public List<OrderDetails>? Products { get; set; }

        public Provider? provider { get; set; }
        public Address? address { get; set; }

        public UserDetailResponse? userDetail { get; set; }
    }
}

public class ResponseManualOrder
{
    public ManualOrder? order { get; set; }


    public Provider? Provider { get; set; }
    public Address? AddressUser { get; set; }

    public UserDetailResponse? UserDetail { get; set; }
}

public class OrderDetails
{

    public OrderItem? Order { get; set; }
    public Product? Product { get; set; }


}

