using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HattliApi.Models;

namespace HatlliApi.ViewModels
{
    public class SearchResponse
    {
        public Product? product { get; set; }

        public Provider? provider { get; set; }
    }
}