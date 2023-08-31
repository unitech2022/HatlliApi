using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using HattliApi.Data;

using HattliApi.Models;
using HattliApi.Models.BaseEntity;
using HattliApi.ViewModels;
using X.PagedList;
using HatlliApi.Helpers;
using HatlliApi.Models;

namespace HattliApi.Serveries.ProvidersService
{
    public class ProvidersService : IProvidersService
    {

        private readonly AppDBcontext _context;
        private IMapper _mapper;

        public ProvidersService(AppDBcontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<dynamic> AddAsync(dynamic type)
        {
            await _context.Providers!.AddAsync(type);

            await _context.SaveChangesAsync();

            return type;

        }

        public async Task<dynamic> DeleteAsync(int typeId)
        {
            Provider? market = await _context.Providers!.FirstOrDefaultAsync(x => x.Id == typeId);

            if (market != null)
            {
                _context.Providers!.Remove(market);

                await _context.SaveChangesAsync();
            }

            return market!;
        }

        public async Task<dynamic> GetItems(string UserId, int page)
        {
            List<Provider> providers = await _context.Providers!.ToListAsync();
            return providers;
        }

        // public async Task<ResponseMarketDetails> GetMarketDetails(int marketId, string userId)
        // {


        //     // List<Product> allproduct=new  List<Product>();
        //     Market? market = await _context.Markets!.FirstOrDefaultAsync(x => x.Id == marketId);
        //     List<Product> products = await _context.Products!.Where(x => x.restaurantId == marketId).ToListAsync();
        //     List<Category> categories = await _context.Categories!.ToListAsync();
        //     foreach (Product item in products)
        //     {
        //         Cart? cart = await _context.Carts!.FirstOrDefaultAsync(t => t.ProductId == item.Id && t.UserId == userId);
        //         if (cart != null)
        //         {
        //             item.IsCart = true;
        //         }


        //     }
        //     ResponseMarketDetails responseMarketDetails = new ResponseMarketDetails
        //     {
        //         Market = market,
        //         Products = products,
        //         Categories = categories
        //     };

        //     return responseMarketDetails;

        // }



        public async Task<List<Provider>> SearchProvider(string textSearch, int addressId)
        {
            Address? address = await _context.Addresses!.FirstOrDefaultAsync(t => t.Id == addressId);
            List<Provider> markets = new List<Provider>();
            List<Provider> allMarkets = await _context.Providers!.Where(p => p.Title!.Contains(textSearch) && p.Status != 1).ToListAsync();
            // Address? userAddress = await _context.Providers!.FirstOrDefaultAsync(x => x.Id == AddressId);
            // foreach (var market in allMarkets)
            // {

            foreach (var item in allMarkets)
            {
                double distance = Functions.GetDistance(item.Lat, address!.Lat, item.Lng, address.Lng);
                item.Distance = distance;
            }

            return allMarkets;

        }

        public async Task<dynamic> GitById(int typeId)
        {
            Provider? provider = await _context.Providers!.FirstOrDefaultAsync(x => x.Id == typeId);
            return provider!;
        }



        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateObject(dynamic category)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Provider>> GetProvidersByFieldId(int fieldId, string UserId)
        {
            List<Provider> providers = new List<Provider>();
            List<Provider> allProviders = new List<Provider>();
            Address? address = await _context.Addresses!.FirstOrDefaultAsync(t => t.UserId == UserId);
            if (fieldId == 0)
            {
                allProviders = await _context.Providers!.Where(t => t.Status == 1).ToListAsync();


            }
            else
            {
                allProviders = await _context.Providers!.Where(t => t.CategoryId == fieldId && t.Status == 1).ToListAsync();
            }


            // git distance
            foreach (var item in allProviders)
            {
                double distance = Functions.GetDistance(item.Lat, address!.Lng, address.Lat, item.Lng);
                if (distance <= item.Area)
                {
                    item.Distance = distance;
                    providers.Add(item);
                }
            }
            return providers.OrderByDescending(t => t.Rate).ToList();
        }

        public async Task<Provider> GitProviderByUserId(string userId)
        {
            Provider? provider = await _context.Providers!.FirstOrDefaultAsync(x => x.UserId == userId);
            return provider!;
        }

        public async Task<dynamic> DetailsProvider(int providerId, string userId)
        {
            List<Category> categories = new List<Category> { };
            Provider? provider = await _context.Providers!.FirstOrDefaultAsync(t => t.Id == providerId);
            User? user = await _context.Users.FirstOrDefaultAsync(t => t.Id == provider!.UserId);
            UserDetailResponse userDetail = _mapper.Map<UserDetailResponse>(user);
            Address? address = await _context.Addresses!.FirstOrDefaultAsync(t => t.UserId == provider!.UserId);



            List<Product> products = await _context.Products!.Where(t => t.ProviderId == providerId && t.Status == 0).ToListAsync();
            //** rate provider


            categories = await _context.Categories!.ToListAsync();


            Address? addressUser = await _context.Addresses!.FirstOrDefaultAsync(t => t.UserId == userId);

            if (addressUser != null)
            {


                double distance = Functions.GetDistance(provider!.Lat, addressUser!.Lng, addressUser.Lat, provider!.Lng);
                provider!.Distance = distance;



            }




            return new
            {
                provider = provider,
                products = products.Take(10).ToList(),
                userDetail = userDetail,
                categories = categories,
                address = address
            };
            //   List<int> idsCategories=provider.CategoryId.s
        }

        public async Task<Provider> UpdateProvider(Provider provider)
        {
            Provider? provider1 = await _context.Providers!.FirstOrDefaultAsync(t => t.Id == provider.Id);
            if (provider1 != null)
            {
                if (provider.NameAdministratorCompany != null)
                {
                    provider1.NameAdministratorCompany = provider.NameAdministratorCompany;
                }

                if (provider.Title != null)
                {
                    provider1.Title = provider.Title;
                }

                if (provider.Email != null)
                {
                    provider1.Email = provider.Email;
                }
                if (provider.Area != provider1.Area)
                {
                    provider1.Area = provider.Area;
                }
                if (provider.ImagePassport != null)
                {
                    provider1.ImagePassport = provider.ImagePassport;
                }

                if (provider.NameAdministratorCompany != null)
                {
                    provider1.NameAdministratorCompany = provider.NameAdministratorCompany;
                }
                if (provider.LogoCompany != null)
                {
                    provider1.LogoCompany = provider.LogoCompany;
                }
                if (provider.CategoryId != provider1.CategoryId)
                {
                    provider1.CategoryId = provider.CategoryId;
                }

                if (provider.IBan != provider1.IBan)
                {
                    provider1.IBan = provider.IBan;
                }

                if (provider.NameBunk != provider1.NameBunk)
                {
                    provider1.NameBunk = provider.NameBunk;
                }

                await _context.SaveChangesAsync();


            }
            return provider1!;
        }

        public async Task<dynamic> ReviewProvider(string userId, string from, int to)
        {
            // **  to 0 = weekly ; 1 monthly  ;  2 yearly

            List<Order> orders = new List<Order>();
            List<Product> products = new List<Product>();
            Provider? provider = await _context.Providers!.FirstOrDefaultAsync(t => t.UserId == userId);
            // ** wallet
            double wallet = provider!.Wallet;
            DateTime startDate = DateTime.Parse(from);
            // Console.Write("frrrrrom +   " + startDate.ToString());
            // ** GET  Orders 
            if (to == 0)
            {

                orders = await _context.Orders!.Where(t => t.CreatedAt >= startDate && t.CreatedAt < startDate.AddDays(7) && t.ProviderId == provider.Id).ToListAsync();
                products = await _context.Products!.Where(t => t.CreatedAt >= startDate && t.CreatedAt < startDate.AddDays(7) && t.ProviderId == provider.Id).ToListAsync();
            }
            else if (to == 1)
            {
                orders = await _context.Orders!.Where(t => t.CreatedAt >= startDate && t.CreatedAt < startDate.AddMonths(1) && t.ProviderId == provider.Id).ToListAsync();
                products = await _context.Products!.Where(t => t.CreatedAt >= startDate && t.CreatedAt < startDate.AddMonths(1) && t.ProviderId == provider.Id).ToListAsync();

            }
            else
            {
                orders = await _context.Orders!.Where(t => t.CreatedAt >= startDate && t.CreatedAt < startDate.AddYears(1) && t.ProviderId == provider.Id).ToListAsync();
                products = await _context.Products!.Where(t => t.CreatedAt >= startDate && t.CreatedAt < startDate.AddYears(1) && t.ProviderId == provider.Id).ToListAsync();
            }

            /// **  must orders area
            //  var grouped = orders.GroupBy(item => item.UserId);
            //  var sorted = grouped.OrderByDescending(group => group.Count());
            Address? address = new Address
            {
                Id = 0,
                Lat = 0.0,
                Lng = 0.0,
                Description = "",
                UserId = "dhf",
                Name = "name",
                CreatedAt = DateTime.Now
            };


            if (orders.Count > 0)
            {
                var grp = orders.GroupBy(i => i.UserId).ToList();
                int max = grp.Max(c => c.Count());
                var most = grp.Where(d => d.Count() == max)
                              .Select(c => c.Key).ToList();

                //     Order? order = most.First();
                //     int countOrder=most.Where(t=> t.UserId==order.UserId).Count();
                address = await _context.Addresses!.FirstOrDefaultAsync(t => t.UserId == most.First());
                address!.Id = max;
            }

            return new
            {
                wallet = wallet,
                ordersAccepted = orders.Where(t => t.Status <= 3).Count(),
                ordersCanceled = orders.Count(),
                products = products.Count(),
                provider = provider,
                most = address

            };
        }

        public async Task<dynamic> BalanceWithdrawalProvider(string userId, double mony, int type)
        {
            Provider? provider = await _context.Providers!.FirstOrDefaultAsync(t => t.UserId == userId);
            if (type == 0)
            {
                provider!.Wallet -= mony;



            }
            else
            {
                mony = provider!.Wallet;
                provider!.Wallet = 0.0;
            }

            OrderWallet orderWallet = new OrderWallet
            {
                UserId = userId,
                Mony = mony,

            };

            await _context.OrderWallets!.AddAsync(orderWallet);

            await _context.SaveChangesAsync();
            return orderWallet;
        }



        // public async Task<BaseResponse> GetMarketsByFieldId(int fieldId, int AddressId, int page)
        // {
        //     List<Market> markets = new List<Market>();
        //     List<Market> allMarkets = await _context.Markets!.Where(t => t.FieldId == fieldId).ToListAsync();
        //     Address? address = await _context.Addresses!.FirstOrDefaultAsync(t => t.Id == AddressId);
        //     foreach (Market item in allMarkets)
        //     {
        //         double distance = Functions.GetDistance(address!.Lat, address.Lng, item.Lat, item.Lng);
        //         if (distance <= 30)
        //         {

        //             markets.Add(item);
        //         }

        //     }

        //     var pageResults = 10f;
        //     var pageCount = Math.Ceiling(markets.Count() / pageResults);

        //     var items = await markets
        //         .Skip((page - 1) * (int)pageResults)
        //         .Take((int)pageResults)
        //         .ToListAsync();



        //     BaseResponse baseResponse = new BaseResponse
        //     {
        //         Items = items,
        //         CurrentPage = page,
        //         TotalPages = (int)pageCount
        //     };

        //     return baseResponse;


        // }



    }
}