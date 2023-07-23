using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
            List<Provider> lastProviders = new List<Provider>();
            User? user = await _context.Users.FirstOrDefaultAsync(t => t.Id == userId);

            int providers = _context.Providers!.ToList().Count;
            int products = _context.Products!.ToList().Count;
            int orders = _context.Orders!.ToList().Count;
            int users = _context.Users!.Where(t => t.Role == "user").ToList().Count;

            if (providers > 10)
            {
                lastProviders = await _context.Providers!.OrderByDescending(t => t.CreatedAt).Skip(10).ToListAsync();
            }
            else
            {
                lastProviders = await _context.Providers!.OrderByDescending(t => t.CreatedAt).ToListAsync();
            }



            UserDetailResponse adminUser = _mapper.Map<UserDetailResponse>(user);

            DashboardHomeResponse dashboardHomeResponse = new DashboardHomeResponse
            {
                AdminDetails = adminUser,
                Providers = providers,
                Products = products,
                Orders = orders,
                LastProviders = lastProviders,
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

            var pageResults = 10f;
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
            List<Provider> providers = new List<Provider>();

            if (textSearch == "not" || textSearch == null)
            {
                providers = await _context.Providers!.ToListAsync();
            }
            else
            {
                providers = await _context.Providers!.Where(t => t.Title!.Contains(textSearch)).ToListAsync();
            }


            var pageResults = 10f;
            var pageCount = Math.Ceiling(providers.Count() / pageResults);

            var items = await providers
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

            var pageResults = 10f;
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

            var pageResults = 10f;
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
    }
}