using System;
using Microsoft.AspNetCore.Identity;

namespace HattliApi.Models
{
    public class User : IdentityUser
    {
        public string? Role { get; set; }
        public string? FullName { get; set; }

      
        public string? DeviceToken { get; set; }
        public int? Status { get; set; }

    
        public string? Code { get; set; }


        // provider
        public string? ProfileImage { get; set; }

        
        public string? City { get; set; }

        public double? Points { get; set; }

        public string? Address { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }

        public double? Rate { get; set; }
        public double? SurveysBalance { get; set; }
        public DateTime? CreatedAt { get; set; }
        public User()
        {
            CreatedAt = DateTime.UtcNow.AddHours(3);
            Status =0;
            Points = 0;
            Lat = 0.0;
            Lng = 0.0;
            Rate = 0.0;
            SurveysBalance = 0;

        }
    }
}

