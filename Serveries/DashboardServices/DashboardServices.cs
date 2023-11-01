using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HatlliApi.Helpers;
using HatlliApi.Models;
using HatlliApi.ViewModels;
using HattliApi.Data;
using HattliApi.Models;
using HattliApi.Models.BaseEntity;
using HattliApi.ViewModels;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace HatlliApi.Serveries.DashboardServices
{
    public class DashboardServices : IDashboardServices
    {
        private readonly IMapper _mapper;


        private readonly AppDBcontext _context;

        public DashboardServices(IMapper mapper, AppDBcontext context)
        {
            _mapper = mapper;

            _context = context;
        }

        public async Task<DashboardHomeResponse> GetHomeDashboard(string userId)
        {
            List<ProviderResponseDashBoard> providerResponseDashBoards = new List<ProviderResponseDashBoard>();
            List<Provider> lastProviders = new List<Provider>();
            User? user = await _context.Users.FirstOrDefaultAsync(t => t.Id == userId);

            int providers = _context.Providers!.ToList().Count;
            int products = _context.Products!.ToList().Count;
            int orders = _context.Categories!.ToList().Count;
            int users = _context.Users!.Where(t => t.Role == "user").ToList().Count;

            if (providers > 10)
            {
                lastProviders = await _context.Providers!.OrderByDescending(t => t.CreatedAt).Take(10).ToListAsync();
            }
            else
            {
                lastProviders = await _context.Providers!.OrderByDescending(t => t.CreatedAt).ToListAsync();
            }

            foreach (var item in lastProviders)
            {
                User? user1 = await _context.Users.FirstOrDefaultAsync(t => t.Id == item.UserId);
                UserDetailResponse userDetails = _mapper.Map<UserDetailResponse>(user1);

                providerResponseDashBoards.Add(new ProviderResponseDashBoard
                {
                    provider = item,
                    userDetail = userDetails
                });
            }



            UserDetailResponse adminUser = _mapper.Map<UserDetailResponse>(user);

            DashboardHomeResponse dashboardHomeResponse = new DashboardHomeResponse
            {
                AdminDetails = adminUser,
                Providers = providers,
                Products = products,
                Orders = orders,
                LastProviders = providerResponseDashBoards,
                Users = users

            };
            return dashboardHomeResponse;

        }

        public async Task<BaseResponse> GetProducts(int page, string textSearch)
        {
            List<ProductResponseAdmin> products = new List<ProductResponseAdmin>();
            List<Product> allProducts = new List<Product>();
            if (textSearch == "not" || textSearch == null)
            {
                allProducts = await _context.Products!.ToListAsync();
            }
            else
            {
                allProducts = await _context.Products!.Where(t => t.Name!.Contains(textSearch)).ToListAsync();
            }

            foreach (var item in allProducts)
            {
                Provider? provider = await _context.Providers!.FirstOrDefaultAsync(t => t.Id == item.ProviderId);

                products.Add(new ProductResponseAdmin
                {
                    provider = provider,
                    product = item
                });


            }

            var pageResults = 30f;
            var pageCount = Math.Ceiling(products.Count() / pageResults);

            var items = await products
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();
            BaseResponse baseResponse = new BaseResponse
            {
                Items = items,
                CurrentPage = page,
                TotalPages = (int)pageCount
            };

            return baseResponse;
        }

        public async Task<BaseResponse> GetProviders(int page, string textSearch)
        {
            List<ProviderResponseDashBoard> providerResponseDashBoards = new List<ProviderResponseDashBoard>();
            List<Provider> providers = new List<Provider>();

            if (textSearch == "not" || textSearch == null)
            {
                providers = await _context.Providers!.ToListAsync();
            }
            else
            {
                providers = await _context.Providers!.Where(t => t.Title!.Contains(textSearch)).ToListAsync();
            }

            foreach (var item in providers)
            {
                User? user1 = await _context.Users.FirstOrDefaultAsync(t => t.Id == item.UserId);
                UserDetailResponse userDetails = _mapper.Map<UserDetailResponse>(user1);

                providerResponseDashBoards.Add(new ProviderResponseDashBoard
                {
                    provider = item,
                    userDetail = userDetails
                });
            }


            var pageResults = 30f;
            var pageCount = Math.Ceiling(providerResponseDashBoards.Count() / pageResults);

            var items = await providerResponseDashBoards
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();
            BaseResponse baseResponse = new BaseResponse
            {
                Items = items,
                CurrentPage = page,
                TotalPages = (int)pageCount
            };

            return baseResponse;
        }

        public async Task<object> UpdateStatusProvider(int providerId, int status)
        {
            Provider? provider = await _context.Providers!.FirstOrDefaultAsync(t => t.Id == providerId);
            if (provider != null)
            {
                provider.Status = status;
                await _context.SaveChangesAsync();
            }
            return provider!;
        }

        public async Task<object> UpdateStatusProduct(int productId, int status)
        {
            Product? product = await _context.Products!.FirstOrDefaultAsync(t => t.Id == productId);
            if (product != null)
            {
                product.Status = status;
                await _context.SaveChangesAsync();
            }
            return product!;
        }

        public async Task<BaseResponse> GetOrders(int page)
        {
            List<OrderResponseAdmin> orders = new List<OrderResponseAdmin>();


            List<Order> allOrders = await _context.Orders!.ToListAsync();


            foreach (var item in allOrders)
            {
                Provider? provider = await _context.Providers!.FirstOrDefaultAsync(t => t.Id == item.ProviderId);

                orders.Add(new OrderResponseAdmin
                {
                    Provider = provider,
                    order = item
                });


            }

            var pageResults = 30f;
            var pageCount = Math.Ceiling(orders.Count() / pageResults);

            var items = await orders
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();
            BaseResponse baseResponse = new BaseResponse
            {
                Items = items,
                CurrentPage = page,
                TotalPages = (int)pageCount
            };

            return baseResponse;
        }

        public async Task<BaseResponse> GetUsers(int page, string textSearch)
        {



            List<UserResponseAdmin> users = new List<UserResponseAdmin>();
            List<User> allUsers = new List<User>();
            if (textSearch == "not" || textSearch == null)
            {
                allUsers = await _context.Users!.Where(t => t.Role == "user")!.ToListAsync();
            }
            else
            {
                allUsers = await _context.Users!.Where(t => t.UserName!.Contains(textSearch) && t.Role == "user").ToListAsync();
            }

            foreach (var item in allUsers)
            {
                Address? address = await _context.Addresses!.FirstOrDefaultAsync(t => t.UserId == item.Id);
                users.Add(new UserResponseAdmin
                {
                    user = item,
                    address = address
                });

            }

            var pageResults = 30f;
            var pageCount = Math.Ceiling(users.Count() / pageResults);

            var items = await users
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();
            BaseResponse baseResponse = new BaseResponse
            {
                Items = items,
                CurrentPage = page,
                TotalPages = (int)pageCount
            };

            return baseResponse;
        }

        public async Task<BaseResponse> GetWallets(int page)
        {
            List<WalletResponse> walletResponses = new List<WalletResponse>();
            List<OrderWallet> orderWallets = await _context.OrderWallets!.OrderByDescending(t => t.CreatedAt).ToListAsync();

            foreach (var item in orderWallets)
            {
                Provider? provider = await _context.Providers!.FirstOrDefaultAsync(t => t.UserId == item.UserId);
                User? user = await _context.Users!.FirstOrDefaultAsync(t => t.Id == item.UserId);
                UserDetailResponse? userDetailResponse = _mapper.Map<UserDetailResponse>(user);
                walletResponses.Add(new WalletResponse
                {
                    wallet = item,
                    userDetail = userDetailResponse,
                    provider = provider
                });

            }

            var pageResults = 30f;
            var pageCount = Math.Ceiling(walletResponses.Count() / pageResults);

            var items = await walletResponses
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();
            BaseResponse baseResponse = new BaseResponse
            {
                Items = items,
                CurrentPage = page,
                TotalPages = (int)pageCount
            };

            return baseResponse;
        }

        public async Task<object> UpdateStatusWallet(int walletId, int status)
        {
            OrderWallet? wallet = await _context.OrderWallets!.FirstOrDefaultAsync(t => t.Id == walletId);
            if (wallet != null)
            {
                wallet.Status = status;
                await _context.SaveChangesAsync();
            }
            return wallet!;
        }

        public async Task<List<Category>> GetCategories()
        {
            List<Category> categories = await _context.Categories!.ToListAsync();

            return categories;
        }

        public async Task<dynamic> PaymentProvider(string userId, double mony, int type)
        {
            Provider? provider = await _context.Providers!.FirstOrDefaultAsync(t => t.UserId == userId);
            if (provider != null)
            {
                // *** minus
                if (type == 0)
                {
                    if (mony == 0)
                    {
                        provider.Wallet = 0.0;

                    }
                    else
                    {
                        provider.Wallet = provider.Wallet - mony;

                    }
                    // ** add 
                }
                else
                {

                    provider.Wallet = provider.Wallet + mony;
                }

            }

            await _context.SaveChangesAsync();
            return provider!;
        }

        public async Task<object> BlockUser(string userId, int status)
        {
           User? user =await _context.Users.FirstOrDefaultAsync(t => t.Id == userId);

           if(user != null ){
            user.Status = status;
            if(status==0){
 await Functions.SendNotificationAsync(_context, user!.Id!, 0, "تنبيه", "تم تفعيل رقم الهاتف", "", "orders");
            }else {
 await Functions.SendNotificationAsync(_context, user!.Id!, 0, "تنبيه", "تم حظر رقم الهاتف", "", "orders");
            }
              
            await _context.SaveChangesAsync();
           }
           return user!;
        }
    }
}