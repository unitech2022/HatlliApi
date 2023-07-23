using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HattliApi.Models;

namespace HatlliApi.ViewModels
{
    public class ProductResponseAdmin
    {
        public Provider? provider { get; set; }

        public Product? product { get; set; }
    }
}