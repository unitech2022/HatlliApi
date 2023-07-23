using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HattliApi.Models;

namespace HatlliApi.ViewModels
{
    public class OrderResponseAdmin
    {
        public Order? order { get; set; }

        public Provider? Provider { get; set; }
    }
}