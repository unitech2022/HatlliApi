using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HatlliApi.Models;
using HattliApi.Models;

namespace HatlliApi.ViewModels
{
    public class FavoriteResponse
    {
        public Favorite? favorite { get; set; }

        public Product? product { get; set; }
    }
}