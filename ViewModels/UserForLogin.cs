using System;
namespace HattliApi.ViewModels
{
	public class UserForLogin
	{
		public string? UserName{ get; set; }
		public string? DeviceToken { get; set; }
		public string? Code { get; set; }

		public string? Role { get; set; }
	}
}

