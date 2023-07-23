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
            List<Provider> allMarkets = await _context.Providers!.Where(p => p.Title!.Contains(textSearch)).ToListAsync();
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
            List<Provider> providers = new List<Provider> { };
            Address? address = await _context.Addresses!.FirstOrDefaultAsync(t => t.UserId == UserId);
            if (fieldId == 0)
            {
                providers = await _context.Providers!.ToListAsync();


            }
            else
            {
                providers = await _context.Providers!.Where(t => t.CategoryId == fieldId).ToListAsync();
            }


            // git distance
            foreach (var item in providers)
            {
                double distance = Functions.GetDistance(item.Lat, address!.Lng, address.Lat, item.Lng);
                item.Distance = distance;
            }
            return providers;
        }

        public async Task<Provider> GitProviderByUserId(string userId)
        {
            Provider? provider = await _context.Providers!.FirstOrDefaultAsync(x => x.UserId == userId);
            return provider!;
        }

        public async Task<dynamic> DetailsProvider(int providerId)
        {
            List<Category> categories = new List<Category> { };
            Provider? provider = await _context.Providers!.FirstOrDefaultAsync(t => t.Id == providerId);
            User? user = await _context.Users.FirstOrDefaultAsync(t => t.Id == provider!.UserId);
            UserDetailResponse userDetail = _mapper.Map<UserDetailResponse>(user);
            Address? address = await _context.Addresses!.FirstOrDefaultAsync(t => t.UserId == userDetail.id);
            List<Product> products = await _context.Products!.Where(t => t.ProviderId == providerId).ToListAsync();
            categories = await _context.Categories!.ToListAsync();

            return new
            {
                provider = provider,
                products = products,
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

                await _context.SaveChangesAsync();


            }
            return provider1!;
        }

        public async Task<dynamic> ReviewProvider(int providerId, string from, int to)
        {
            // **  to 0 = weekly ; 1 monthly  ;  2 yearly

            List<Order> orders = new List<Order>();
            Provider? provider = await _context.Providers!.FirstOrDefaultAsync(t => t.Id == providerId);
            // ** wallet
            double wallet = provider!.Wallet;
            DateTime startDate = DateTime.Parse(from);
            Console.Write("frrrrrom +   " + startDate.ToString());
            // ** GET  Orders 
            if (to == 0)
            {

                orders = await _context.Orders!.Where(t => t.CreatedAt >= startDate && t.CreatedAt < startDate.AddDays(7) && t.ProviderId == providerId).ToListAsync();
            }
            else if (to == 1)
            {
                orders = await _context.Orders!.Where(t => t.CreatedAt >= startDate && t.CreatedAt < startDate.AddMonths(1) && t.ProviderId == providerId).ToListAsync();

            }else {
                  orders = await _context.Orders!.Where(t => t.CreatedAt >= startDate && t.CreatedAt < startDate.AddYears(1) && t.ProviderId == providerId).ToListAsync();
            }

            return new
            {
                wallet = wallet,
                ordersAccepted = orders.Where(t=>t.Status<=3&& t.Status!=0).Count(),
                 ordersCanceled = orders.Where(t=>t.Status==4).Count()
                
            };
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