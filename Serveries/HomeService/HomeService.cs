using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HattliApi.Data;
using AutoMapper;
using HattliApi.Models;
using Microsoft.EntityFrameworkCore;
using HattliApi.ViewModels;
using X.PagedList;
using HatlliApi.ViewModels;
using HatlliApi.Helpers;
using HatlliApi.Models;

namespace HattliApi.Serveries.HomeService
{

    public class HomeService : IHomeService
    {

        private readonly IMapper _mapper;

        private readonly AppDBcontext _context;

        public HomeService(IMapper mapper, AppDBcontext context)
        {
            _mapper = mapper;
            _context = context;
        }



        public async Task<ResponseHomeProvider> GetHomeDataProvider(string UserId)
        {
            List<OrderHome> orders = new() { };
            List<Product> products = new() { };
            //** address
            Address? address = await _context.Addresses!.FirstOrDefaultAsync(t => t.UserId == UserId);
            //** user
            User? user = await _context.Users!.FirstOrDefaultAsync(t => t.Id == UserId);
            UserDetailResponse userDetail = _mapper.Map<UserDetailResponse>(user);

            List<Alert> alerts = await _context.Alerts!.Where(t => t.UserId == UserId && !t.Viewed).ToListAsync();
            //** provider
            Provider? provider = await _context.Providers!.FirstOrDefaultAsync(t => t.UserId == UserId);

            //** categories
            List<Category> categories = await _context.Categories!.ToListAsync();

            if (provider != null)
            {
                //** products
                products = await _context.Products!.OrderByDescending(t => t.CreatedAt).Where(t => t.ProviderId == provider!.Id).ToListAsync();

                // ** orders 

                List<Order> allOrders = await _context.Orders!.OrderByDescending(t => t.CreatedAt).Where(t => t.ProviderId == provider!.Id).ToListAsync();

                foreach (var item in allOrders)
                {
                    User? userOrder = await _context.Users!.FirstOrDefaultAsync(t => t.Id == item.UserId);
                    Address? addressUser = await _context.Addresses!.FirstOrDefaultAsync(t => t.UserId == item.UserId);
                    orders.Add(new OrderHome
                    {
                        order = item,
                        name = userOrder!.FullName,
                        imageUrl = userOrder.ProfileImage,
                        address = addressUser

                    });

                }
            }


            return new ResponseHomeProvider
            {
                orders = orders,
                Products = products.Take(10).ToList(),
                provider = provider,
                user = userDetail,
                address = address,
                NotiyCount = alerts.Count(),
                categories = categories
            };


        }

        public async Task<ResponseHomeUser> GetHomeUserData(string UserId,string location)
        {
            List<Order> allOrders = new() { };
            List<Provider> providers = new() { };
            List<OrderHome> orders = new() { };
            //** address
            Address? address = await _context.Addresses!.FirstOrDefaultAsync(t => t.UserId == UserId);
            //** categories
            List<Category> categories = await _context.Categories!.ToListAsync();
            //** get user
            User? user = await _context.Users!.FirstOrDefaultAsync(t => t.Id == UserId);
            UserDetailResponse userDetail = _mapper.Map<UserDetailResponse>(user);
            List<Alert> alerts = await _context.Alerts!.Where(t => t.UserId == UserId && !t.Viewed).ToListAsync();
            // ** carts
            List<Cart> carts = await _context.Carts!.Where(t => t.UserId == UserId).ToListAsync();
            //** get providers
            List<Provider> allProviders = await _context.Providers!.Where(t => t.Status == 1 && t.Wallet > -100).ToListAsync();
            // get distance
            if (address != null)
            {

                foreach (Provider item in allProviders)
                {

                    double distance = Functions.GetDistance(item.Lat, address!.Lng, address.Lat, item.Lng);
                    if (distance <= item.Area)
                    {
                        item.Distance = distance;
                        providers.Add(item);
                    }




                }
            }
            else if(location!=null)
            {
                double lat=double.Parse(location.Split(",")[0]);
                 double lng=double.Parse(location.Split(",")[1]);
                
                foreach (Provider item in allProviders)
                {

                    double distance = Functions.GetDistance(item.Lat, lng, lat, item.Lng);
                    if (distance <= item.Area)
                    {
                        item.Distance = distance;
                        providers.Add(item);
                    }
                }}

                // ** orders
                allOrders = await _context.Orders!.OrderByDescending(t => t.CreatedAt).Where(t => t.UserId == UserId).ToListAsync();
                foreach (var item in allOrders)
                {
                    Provider? provider1 = await _context.Providers!.FirstOrDefaultAsync(t => t.Id == item.ProviderId);
                    orders.Add(new OrderHome
                    {
                        order = item,
                        name = provider1!.Title,
                        imageUrl = provider1!.LogoCompany
                    });

                }

                // ** favorites

                List<Favorite> favorites = await _context.Favorites!.Where(t => t.UserId == UserId).ToListAsync();
                return new ResponseHomeUser
                {
                    address = address,
                    providers = providers,
                    orders = orders,
                    NotiyCount = alerts.Count(),
                    user = userDetail,
                    favorites = favorites,
                    carts = carts,
                    categories = categories,

                };



            }


        }
    }