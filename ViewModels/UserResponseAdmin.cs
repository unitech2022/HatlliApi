using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HattliApi.Models;

namespace HatlliApi.ViewModels
{
    public class UserResponseAdmin
    {
        public User? user { get; set; }
        public Address? address { get; set; }
    }
}