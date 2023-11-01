using HattliApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using HattliApi.Serveries;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;

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
    public class UsersController : Controller
    {
        private readonly IUserService? _service;
        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("check-username")]
        public async Task<Object> IsUserRegistered([FromForm] string UserName, [FromForm] string userRole)
        {
            
            object result = await _service!.IsUserRegistered(UserName, userRole);
            return Ok(result);
        }

        [HttpPost("signup")]
        public async Task<ActionResult> Register([FromForm] UserForRegister userForRegister)
        {
            dynamic result = await _service!.Register(userForRegister);
            return result.status == false ? (ActionResult)Ok(result) : (ActionResult)Ok(result);
        }


        // 	userName = "tall3at",
        // numbers = userName,
        // userSender = "tall3at",
        // apiKey = "35cc2cd49e6eaf1a17ff287b3a8b7df3",
        // msg = "رمز التحقق: "+ code 

        [HttpPost("send-sms")]
        public async Task<ActionResult> sendSms([FromForm] string code, [FromForm] string userName)
        {

            using HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var data = new

            {
                userName = "hatli",
                numbers = userName,
                userSender = "auth-mseg",
                apiKey = "804ecc4281295b4dbda0b8a01fdffce0",
                msg = "رمز التحقق: " + code
            };

            string json = JsonConvert.SerializeObject(data);
            StringContent httpContent = new(json, Encoding.UTF8, "application/json");

            HttpResponseMessage result = await client.PostAsync("https://www.msegat.com/gw/sendsms.php", httpContent);
            System.Diagnostics.Debug.WriteLine(result.Content.ReadAsStringAsync());
            return Ok(result.Content.ReadAsStringAsync());
        }

        [HttpPost("user-login")]
        public async Task<IActionResult> LoginUser([FromForm] UserForLogin userForLogin)
        {
            dynamic result = await _service!.LoginUser(userForLogin);
            return result.status == false ? Unauthorized() : (IActionResult)Ok(result);
        }

        // [Authorize(Roles ="user")]
        [HttpPost("update-user")]
        public async Task<ActionResult> UpdateUser([FromForm] UserForUpdate userForUpdate)
        {
            object result = await _service!.UpdateUser(userForUpdate);
            return Ok(result);
        }

        // [Authorize(Roles ="user")]
        [HttpPost("update-device-token")]
        public async Task<ActionResult> UpdateDeviceToken([FromForm] string Token, [FromForm] string UserId)
        {
            bool result = await _service!.UpdateDeviceToken(Token, UserId);
            return Ok(result);
        }


        // [Authorize(Roles = "user")]
        [HttpPost("get-user")]
        public async Task<ActionResult> GetUser([FromForm] string UserId)
        {
            UserDetailResponse user = await _service!.GetUser(UserId);
            return Ok(user);
        }


        [HttpPost("admin-signup")]
        public async Task<ActionResult> RegisterAdmin([FromForm] UserForRegister userForRegister)
        {
            dynamic result = await _service!.RegisterAdmin(userForRegister);
            return result.status == true ? (ActionResult)BadRequest(result) : (ActionResult)Ok(result);
        }


        [HttpPost("admin-login")]
        public async Task<IActionResult> LoginAdmin([FromForm] AdminForLoginRequest adminForLogin)
        {
            dynamic result = await _service!.LoginAdmin(adminForLogin);
            return result == null ? Unauthorized() : (IActionResult)Ok(result);
        }

          [HttpPost("delete-account")]
        public async Task<IActionResult> DeleteAccount([FromForm] string userId)
        {
          
            return Ok(await _service!.DelateAccount(userId));
        }
    }
}

