using System;
using HattliApi.Models;
using HattliApi.ViewModels;

namespace HattliApi.Serveries
{
	public interface IUserService
	{
		Task<object> Register(UserForRegister userForRegister);
		Task<object> IsUserRegistered(string UserName,string userRole);
		Task<object> LoginAdmin(AdminForLoginRequest adminForLogin);
		Task<object> LoginUser(UserForLogin userForLogin);
		Task<object> RegisterAdmin(UserForRegister adminForRegister);
		Task<object> UpdateUser(UserForUpdate userForUpdate);

		Task<bool> UpdateDeviceToken(string Token,string UserId);

		Task<User> DelateAccount(string UserId);
		Task<UserDetailResponse> GetUser(string UserId);
	}
}

