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




namespace HatlliApi.Serveries.ManualOrdersServices
{
    public class ManualOrdersServices : IManualOrdersServices
    {

        private readonly IMapper _mapper;


        private readonly AppDBcontext _context;


        // private readonly IAddressesServices _AddressesServices;
        // private readonly IMarketsService _IMarketsService;
        // -1 =>  "تم الالغاء"
        //  0 =>  "في انتظار التأكيد"
        // 1=>  "تم تأكيد طلبك"
        // 2 => "جارى التجهيز"
        // 3 => "تم التجهيز"
        // 4 =>  "جارى التوصيل"
        // 5 =>  "تم التسليم"


        List<string> ManualOrderStatuses = new List<string>(new string[] { "في انتظار التأكيد", "تم تأكيد طلبك", "جارى التجهيز", "تم التسليم", "تم الغاء الطلب" });
        public ManualOrdersServices(IMapper mapper, AppDBcontext context
        // IAddressesServices addressesServices,
        //  IMarketsService iMarketsService
         )
        {
            _mapper = mapper;
            _context = context;
            // _AddressesServices = addressesServices;
            // _IMarketsService = iMarketsService;
        }
        public async Task<dynamic> AddAsync(dynamic type)
        {


            ManualOrder? ManualOrder = await _context.ManualOrders!.AddAsync(type);
            Provider? market = await _context.Providers!.FirstOrDefaultAsync(t => t.Id == ManualOrder.ProviderId);
            //    User? provider=await _context.Users!.FirstOrDefaultAsync(t => t.Id==market!.UserId);
            // await Functions.SendNotificationAsync(_context, market!.UserId!, "طلب جديد", "تم ارسال طلب جديد", "");

            await _context.SaveChangesAsync();

            return type;
        }

        public async Task<dynamic> AddOrder(ManualOrder ManualOrder)
        {
            Provider? provider = await _context.Providers!.FirstOrDefaultAsync(t => t.Id == ManualOrder.ProviderId);

            await _context.ManualOrders!.AddAsync(ManualOrder);
            _context.SaveChanges();
            await Functions.SendNotificationAsync(_context, provider!.UserId!, ManualOrder.Id, "طلب جديد", "تم ارسال طلب جديد", "", "ManualOrders");
            return ManualOrder;
        }


        public async Task<dynamic> DeleteAsync(int typeId)
        {
            ManualOrder? ManualOrder = await _context.ManualOrders!.FirstOrDefaultAsync(x => x.Id == typeId);

            if (ManualOrder != null)
            {
                _context.ManualOrders!.Remove(ManualOrder);

                await _context.SaveChangesAsync();
            }

            return ManualOrder!;
        }

        public async Task<dynamic> UpdateOrderStatus(int typeId, int status, int sender)
        {

            //** sender ==0 user || 1  provider
            ManualOrder? ManualOrder = await _context.ManualOrders!.FirstOrDefaultAsync(x => x.Id == typeId);
            Provider? provider = await _context.Providers!.FirstOrDefaultAsync(t => t.Id == ManualOrder!.ProviderId);
            if (ManualOrder != null)
            {
                ManualOrder.Status = status;
                double tax = ManualOrder.TotalCost * .15;
                if (status == 4)
                {



                    provider!.Wallet += tax;


                }

                if (status == 1)
                {



                    provider!.Wallet -= tax;
                }
            }

            if (sender == 0)
            {
                await Functions.SendNotificationAsync(_context, provider!.UserId!, ManualOrder!.Id, "تعديل حالة الطلب", ManualOrderStatuses[status], "", "ManualOrders");


            }
            else
            {



                await Functions.SendNotificationAsync(_context, ManualOrder!.UserId!, ManualOrder.Id, "تعديل حالة الطلب", ManualOrderStatuses[status], "", "ManualOrders");







                await _context.SaveChangesAsync();
            }

            return ManualOrder!;
        }



        public async Task<dynamic> GetItems(string UserId, int page)
        {
            List<ManualOrder> ManualOrders = await _context.ManualOrders!.Where(i => i.UserId == UserId).ToListAsync();


            var pageResults = 10f;
            var pageCount = Math.Ceiling(ManualOrders.Count() / pageResults);

            var items = await ManualOrders
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

        public async Task<dynamic> GitById(int typeId)
        {
            ManualOrder? ManualOrder = await _context.ManualOrders!.FirstOrDefaultAsync(x => x.Id == typeId);
            return ManualOrder!;
        }

        public async Task<dynamic> GitOrderDetails(int ManualOrderId)
        {

            ManualOrder? ManualOrder = await _context.ManualOrders!.FirstOrDefaultAsync(t => t.Id == ManualOrderId);
            Address? address = await _context.Addresses!.FirstOrDefaultAsync(t => t.UserId == ManualOrder!.UserId);
            User? user = await _context.Users.FirstOrDefaultAsync(t => t.Id == ManualOrder!.UserId);
            UserDetailResponse userDetailResponse = _mapper.Map<UserDetailResponse>(user);
            Provider? market = await _context.Providers!.FirstOrDefaultAsync(t => t.Id == ManualOrder!.ProviderId);



            return new ResponseManualOrder
            {

                Provider = market,
                order = ManualOrder,
                AddressUser = address,
                UserDetail = userDetailResponse
            };

        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateObject(dynamic category)
        {
            // nothing
        }

        public async Task<dynamic> GitOrdersByMarketId(int marketId)
        {
            List<ManualOrder> ManualOrders = await _context.ManualOrders!.OrderByDescending(t => t.CreatedAt).Where(t => t.ProviderId == marketId).ToListAsync();


            return new
            {
                successManualOrders = ManualOrders.Where(t => t.Status <= 3 && t.Status != 0).ToList(),
                unsuccessfulManualOrders = ManualOrders.Where(t => t.Status == 4).ToList(),
            };
        }

        public async Task<dynamic> GetOrdersUser(string userId)
        {
            List<ManualOrder> ManualOrders = await _context.ManualOrders!.OrderByDescending(t => t.CreatedAt).Where(i => i.UserId == userId).ToListAsync();

            return ManualOrders;
        }









    }




}