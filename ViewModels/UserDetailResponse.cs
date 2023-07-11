using System;
namespace HattliApi.ViewModels
{
	public class UserDetailResponse
	{
          public string? id { get; set; }

            public string? userName { get; set; }
         public string? Role { get; set; }
        public string? FullName { get; set; }
  public string? Email { get; set; }
        public string? NameAdministratorCompany { get; set; }
        public string? DeviceToken { get; set; }
        public int? Status { get; set; }

         public string? About { get; set; }
        public string? Code { get; set; }


        // provider
        public string? ProfileImage { get; set; }

      

        ///     ======================= 
        public string? TypeService { get; set; }
        public string? City { get; set; }

        public double? Points { get; set; }

        public string? Address { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }

        public double? Rate { get; set; }
        public double? SurveysBalance { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}

