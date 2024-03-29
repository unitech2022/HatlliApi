﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using HattliApi.ViewModels;
using HattliApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using HattliApi.Data;


namespace HattliApi.Serveries
{
    public class UserService : IUserService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private UserManager<User> userManager;
        private readonly IConfiguration _config;
        private readonly AppDBcontext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration _config, AppDBcontext context
             )
        {
            this._roleManager = roleManager;
            this._mapper = mapper;
            this.userManager = userManager;
            this._config = _config;
            this._context = context;
            _httpContextAccessor = httpContextAccessor;
        }


        public string RandomNumber()
        {
            Random r = new Random();
            int randNum = r.Next(1000);
            string fourDigitNumber = randNum.ToString("D4");
            return fourDigitNumber;
        }

        public async Task<dynamic> GenerateTokenAsync(User loginUser)
        {
            var userRoles = await userManager.GetRolesAsync(loginUser);
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, loginUser.Id),
                    new Claim(ClaimTypes.Name, loginUser.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1000),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }

        public async Task<bool> ValidateAuth(string authUserId)
        {
            string userId = _httpContextAccessor.HttpContext.User!.FindFirst(ClaimTypes.NameIdentifier).Value;

            return userId == authUserId;
        }

        public async Task SendSms(string Code, string UserName)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                var data = new

                {
                    userName = "tall3at",
                    numbers = UserName,
                    userSender = "tall3at",
                    apiKey = "35cc2cd49e6eaf1a17ff287b3a8b7df3",
                    msg = "رمز التحقق: " + Code

                };

                var json = JsonConvert.SerializeObject(data);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                var result = await client.PostAsync("https://www.msegat.com/gw/sendsms.php", httpContent);
                System.Diagnostics.Debug.WriteLine(result.Content.ReadAsStringAsync());
            }
        }
        public async Task<object> IsUserRegistered(string UserName, string userRole)
        {
            User? user = await _context.Users!.Where(x => x.UserName == UserName).FirstOrDefaultAsync();
            string Code = RandomNumber();

            if (user != null)
            {
                if (user.Status == 2)
                {
                    return new
                    {
                        status = 2, // *** deleted account
                        Code = "0000",
                    };
                }

                else
                {
                    user.Code = Code;
                    await _context.SaveChangesAsync();

                    await SendSms(Code, UserName);


                    return new
                    {
                        status = 1,
                        Code = Code,
                    };
                }


            }
            else
            {

                UserForRegister userForRegister = new UserForRegister
                {
                    Password = "Abc123@",
                    Role = userRole,
                    UserName = UserName
                };


                var loginUser = _mapper.Map<User>(userForRegister);
                loginUser.Role = userForRegister.Role;
                if (!await _roleManager.RoleExistsAsync(userForRegister.Role))
                    await _roleManager.CreateAsync(new IdentityRole(userForRegister.Role));
                var result = await userManager.CreateAsync(loginUser, userForRegister.Password);
                await userManager.AddToRoleAsync(loginUser, userForRegister.Role);
                loginUser.Code = Code;
                await _context.SaveChangesAsync();

                await SendSms(Code, UserName);



                return new
                {
                    status = 0,
                    Code = Code,
                };
            }

        }

        public async Task<dynamic> Register(UserForRegister userForRegister)
        {
            // dynamic userExist =await IsUserExist(userForRegister);
            // if (userExist != "") {
            //     return new {
            //         message = userExist,
            //         status=false
            //     };
            // }
            userForRegister.Password = "Abc123@";
            var userToCreate = _mapper.Map<User>(userForRegister);
            userToCreate.Role = userForRegister.Role;
            if (!await _roleManager.RoleExistsAsync(userForRegister.Role))
                await _roleManager.CreateAsync(new IdentityRole(userForRegister.Role));
            var result = await userManager.CreateAsync(userToCreate, userForRegister.Password);
            await userManager.AddToRoleAsync(userToCreate, userForRegister.Role);
            string Code = RandomNumber();
            userToCreate.Code = Code;
            await _context.SaveChangesAsync();
            return new { message = Code, status = true };
        }


        public async Task<dynamic> IsUserExist(UserForRegister userForValidate)
        {
            string error = "";
            User? user = await _context.Users!.Where(x => x.UserName == userForValidate.UserName).FirstOrDefaultAsync();
            if (user != null)
            {
                error = "رقم الهاتف مسجل من قبل";
                return error;
            }
            // user = await _context.Users.Where(x => x.Email == userForValidate.Email).FirstOrDefaultAsync();
            // if (user != null)
            // {
            //     error = "البريد الإلكتروني مسجل من قبل";
            //     return error;


            // }
            return error;

        }

        public async Task<object> LoginAdmin(AdminForLoginRequest adminForLogin)
        {
            var loginUser = await userManager.FindByNameAsync(adminForLogin.UserName);
            if (loginUser != null && await userManager.CheckPasswordAsync(loginUser, adminForLogin.Password))
            {
                var Token = await GenerateTokenAsync(loginUser);
                return new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(Token),
                    user = _mapper.Map<UserDetailResponse>(loginUser),
                    expiration = Token.ValidTo,

                };
            }
            return loginUser!;
        }

        public async Task<dynamic> LoginUser(UserForLogin userForLogin)
        {

            var loginUser = await userManager.FindByNameAsync(userForLogin.UserName);

            if (loginUser == null)
            {
                return new
                {
                    status = false
                };
                // Address? address=await _context.Addresses!.FirstOrDefaultAsync(t => t.UserId == loginUser.Id && t.DefaultAddress==true);
                // UserForRegister userForRegister = new UserForRegister
                // {
                //     Password = "Abc123@",
                //     Role = userForLogin.Role,
                //     UserName = userForLogin.UserName
                // };


                //     loginUser = _mapper.Map<User>(userForRegister);
                //     loginUser.Role = userForRegister.Role;
                //     if (!await _roleManager.RoleExistsAsync(userForRegister.Role))
                //         await _roleManager.CreateAsync(new IdentityRole(userForRegister.Role));
                //     var result = await userManager.CreateAsync(loginUser, userForRegister.Password);
                //     await userManager.AddToRoleAsync(loginUser, userForRegister.Role);
                //     string Code = RandomNumber();
                //     loginUser.Code = Code;
                //     await _context.SaveChangesAsync();
            }
            else
            {



                // login 
                if (userForLogin.Code == loginUser.Code || userForLogin.Code == "0000")
                {

                    loginUser.DeviceToken = userForLogin.DeviceToken;
                    await _context.SaveChangesAsync();

                    var Token = await GenerateTokenAsync(loginUser);
                    return new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(Token),
                        user = _mapper.Map<UserDetailResponse>(loginUser),
                        expiration = Token.ValidTo,
                        status = true,
                        // address =address
                    };
                }
                else
                {
                    return new
                    {
                        status = false
                    };
                }
            }

        }

        public async Task<bool> UpdateDeviceToken(string Token, string UserId)
        {
            User user = await _context.Users.Where(x => x.Id == UserId).FirstAsync();
            user.DeviceToken = Token;
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<object> UpdateUser(UserForUpdate userForUpdate)
        {
            User? user = await _context.Users!.Where(x => x.Id == userForUpdate.UserId).FirstOrDefaultAsync();
            // if (user == null) return false;
            if (userForUpdate.FullName != null)
            {
                user!.FullName = userForUpdate.FullName;
            }
            if (userForUpdate.ProfileImage != null)
            {
                user.ProfileImage = userForUpdate.ProfileImage;
            }
            // if (userForUpdate.Points != null)
            // {
            //     user.Points = user.Points+userForUpdate.Points;
            // }

            if (userForUpdate.City != null)
            {
                user!.City = userForUpdate.City;
            }
            // if (userForUpdate.Birth != null)
            // {
            //     user.Birth = userForUpdate.Birth;
            // }

            await _context.SaveChangesAsync();
            return user!;
        }
        public async Task<object> RegisterAdmin(UserForRegister userForRegister)
        {

            dynamic userExist = await IsUserExist(userForRegister);
            if (userExist != "")
            {
                return new
                {
                    error = userExist,
                    status = false
                };
            }
            var userToCreate = _mapper.Map<User>(userForRegister);
            userToCreate.Role = userForRegister.Role;
            if (!await _roleManager.RoleExistsAsync(userForRegister.Role))
                await _roleManager.CreateAsync(new IdentityRole(userForRegister.Role));
            var result = await userManager.CreateAsync(userToCreate, userForRegister.Password);
            await userManager.AddToRoleAsync(userToCreate, userForRegister.Role);

            await _context.SaveChangesAsync();
            return new { status = true };
        }

        public async Task<UserDetailResponse> GetUser(string UserId)
        {
            User? user = await _context.Users!.FindAsync(UserId);
            UserDetailResponse userDetail = _mapper.Map<UserDetailResponse>(user);
            return userDetail!;
        }

        public async Task<User> DelateAccount(string UserId)
        {
            User? user = await _context.Users!.Where(x => x.Id == UserId).FirstOrDefaultAsync();
            if (user != null)
            {

                user.Status = 2;
                await _context.SaveChangesAsync();


            }
            return user!;
        }
    }
}

