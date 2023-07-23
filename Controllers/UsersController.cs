using System;
using HattliApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HattliApi.Serveries;


namespace HattliApi.Controllers
{


	//  dotnet commends

// dotnet publish --configuration Release
// migrations dotnet
// dotnet ef migrations add InitialCreate
 // update database 
// dotnet ef database update
// create
// dotnet new webapi -n name 
	public class UsersController :Controller
	{
		private readonly IUserService? _service;
		public UsersController(IUserService service) {
			_service = service;
		}

		[HttpPost("check-username")]
		public async Task<Object> IsUserRegistered([FromForm]string UserName)
		{
			var result = await _service!.IsUserRegistered(UserName);
			return Ok(result);
		}

		[HttpPost("signup")]
		public async Task<ActionResult> Register([FromForm] UserForRegister userForRegister)
		{
			dynamic result = await _service!.Register(userForRegister);
            if (result.status == false)
            {
				return Ok(result);
            }
			return Ok(result);
		}

		[HttpPost("user-login")]
		public async Task<IActionResult> LoginUser([FromForm] UserForLogin userForLogin)
		{
			dynamic result = await _service!.LoginUser(userForLogin);
			if (result.status == false)
			{
				return Unauthorized();
			}
			return Ok(result);
		}

		// [Authorize(Roles ="user")]
		[HttpPost("update-user")]
		public async Task<ActionResult> UpdateUser([FromForm] UserForUpdate userForUpdate)
		{
			var result = await _service!.UpdateUser(userForUpdate);
			return Ok(result);
		}

		// [Authorize(Roles ="user")]
		[HttpPost("update-device-token")]
		public async Task<ActionResult> UpdateDeviceToken([FromForm] string Token,[FromForm]  string UserId)
		{
			var result = await _service!.UpdateDeviceToken(Token,UserId);
			return Ok(result);
		}


		// [Authorize(Roles = "user")]
		[HttpPost("get-user")]
		public async Task<ActionResult> GetUser([FromForm] string UserId)
		{
			var user = await _service!.GetUser(UserId);
			return Ok(user);
		}


		[HttpPost("admin-signup")]
		public async Task<ActionResult> RegisterAdmin([FromForm] UserForRegister userForRegister)
		{
			dynamic result = await _service!.RegisterAdmin(userForRegister);
			if (result.status == true)
			{
				return BadRequest(result);
			}
			return Ok(result);
		}


		[HttpPost("admin-login")]
		public async Task<IActionResult> LoginAdmin([FromForm] AdminForLoginRequest adminForLogin)
		{
			dynamic result = await _service!.LoginAdmin(adminForLogin);
            if (result == null)
            {
				return Unauthorized();
            }
			return Ok(result);
		}
	}
	}

