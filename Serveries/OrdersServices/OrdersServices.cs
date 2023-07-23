using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;
using HatlliApi.Helpers;
using HatlliApi.ViewModels;
using HattliApi.Data;
using HattliApi.Models;
using HattliApi.Models.BaseEntity;
using Microsoft.EntityFrameworkCore;

using X.PagedList;




namespace HatlliApi.Serveries.OrdersServices
{
    public class OrdersServices : IOrdersServices
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


        List<string> orderStatuses = new List<string>(new string[] { "في انتظار التأكيد", "تم تأكيد طلبك", "جارى التجهيز", "تم التسليم", "تم الغاء الطلب" });
        public OrdersServices(IMapper mapper, AppDBcontext context
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


            Order? order = await _context.Orders!.AddAsync(type);
            Provider? market = await _context.Providers!.FirstOrDefaultAsync(t => t.Id == order.ProviderId);
            //    User? provider=await _context.Users!.FirstOrDefaultAsync(t => t.Id==market!.UserId);
            // await Functions.SendNotificationAsync(_context, market!.UserId!, "طلب جديد", "تم ارسال طلب جديد", "");

            await _context.SaveChangesAsync();

            return type;
        }

        public async Task<dynamic> AddOrder(string userId, int payment)
        {
            List<Cart> carts = await _context.Carts!.Where(x => x.UserId == userId).ToListAsync();


            // AppConfig appConfigDeliveryCost = await getConfigKey("deliveryCostUnit");
            // AppConfig appConfigTax = await getConfigKey("taxRatio");

            //  Address? address = await _context.Addresses!.FirstOrDefaultAsync(t=> t.UserId==order.UserId);
            Product? product = await _context.Products!.FirstOrDefaultAsync(t => t.Id == carts.First().ProductId);
            Provider? market = await _context.Providers!.FirstOrDefaultAsync(t => t.Id == carts.First().ProviderId);

            // double distance = Functions.GetDistance(address.Lat, market.Lat, address.Lng, market.Lng);
            double productsCost = carts.Sum(i => i.Cost);
            // double.Parse(appConfigDeliveryCost.Value ?? "0.0") 
            // double deliveryCost = 0.0 * distance;

            double tax = productsCost * .15;

            // double totalCost = productsCost + deliveryCost + tax;

            if (payment == 0)
            {

                market!.Wallet -= tax;


            }
            else
            {
                double points = productsCost - tax;
                market!.Wallet += points;
            }


            Order order = new Order
            {
                ProviderId = market!.Id,
                UserId = userId,
                Notes = "",
                payment = payment,
                TotalCost = productsCost,
                ProductsCost = productsCost,
            };





            await _context.Orders!.AddAsync(order);
            _context.SaveChanges();

            var orderItemsToAdd = carts.ConvertAll(c => new OrderItem()
            {
                ProductId = c.ProductId,
                Quantity = c.Quantity,
                Cost = c.Cost,
                UserId = c.UserId,
                OrderId = order.Id,
                // Options = c.Options,
                CreatedAt = c.CreatedAt

            });

            _context.OrderItems!.AddRange(orderItemsToAdd);
            _context.Carts!.RemoveRange(carts);
            _context.SaveChanges();
            await Functions.SendNotificationAsync(_context, market!.UserId!, order.Id, "طلب جديد", "تم ارسال طلب جديد", "");
            return order;
        }


        public async Task<dynamic> DeleteAsync(int typeId)
        {
            Order? order = await _context.Orders!.FirstOrDefaultAsync(x => x.Id == typeId);

            if (order != null)
            {
                _context.Orders!.Remove(order);

                await _context.SaveChangesAsync();
            }

            return order!;
        }

        public async Task<dynamic> UpdateOrderStatus(int typeId, int status, int sender)
        {

            //** sender ==0 user || 1  provider
            Order? order = await _context.Orders!.FirstOrDefaultAsync(x => x.Id == typeId);
            Provider? provider = await _context.Providers!.FirstOrDefaultAsync(t => t.Id == order!.ProviderId);
            if (order != null)
            {
                order.Status = status;

                if (status == 4)
                {
                    double tax = order.ProductsCost * .15;
                    if (order.payment == 0)
                    {

                        provider!.Wallet += tax;


                    }
                    else
                    {
                        double points = order.ProductsCost - tax;
                        provider!.Wallet -= points;
                    }
                }
                // User? driver = await _context.Users.FirstOrDefaultAsync(t => t.Id == order.DriverId);
                // if (driver != null)
                // {
                //     if (status != 4)
                //     {
                //         driver.Status = "Active";
                //     }
                // }
                if (sender == 0)
                {
                    await Functions.SendNotificationAsync(_context, order.UserId!, order.Id, "تعديل حالة الطلب", orderStatuses[status], "");


                }
                else
                {



                    await Functions.SendNotificationAsync(_context, provider!.UserId!, order.Id, "تعديل حالة الطلب", orderStatuses[status], "");




                }


                await _context.SaveChangesAsync();
            }

            return order!;
        }

        public async Task<dynamic> PaymentOrder(int orderId, int payment)
        {
            Order? order = await _context.Orders!.FirstOrDefaultAsync(x => x.Id == orderId);
            Provider? provider = await _context.Providers!.FirstOrDefaultAsync(t => t.Id == order!.ProviderId);

            double tax = order!.TotalCost! * 15 / 100;
            // ** payment == 0  cash 

            if (payment == 0)
            {
                provider!.Wallet -= tax;


            }
            else
            {
                double points = order!.TotalCost! - tax;
                provider!.Wallet += points;
            }
            order.Status = 3;
            await _context.SaveChangesAsync();
            await Functions.SendNotificationAsync(_context, order!.UserId!, order.Id, "تعديل حالة الطلب", orderStatuses[3], "");

            return order;


        }

        public async Task<dynamic> GetItems(string UserId, int page)
        {
            List<Order> orders = await _context.Orders!.Where(i => i.UserId == UserId).ToListAsync();


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

        public async Task<dynamic> GitById(int typeId)
        {
            Order? order = await _context.Orders!.FirstOrDefaultAsync(x => x.Id == typeId);
            return order!;
        }

        public async Task<dynamic> GitOrderDetails(int orderId)
        {
            List<OrderItem> orderItems = await _context.OrderItems!.Where(t => t.OrderId == orderId).ToListAsync();


            List<OrderDetails> orderDetails = new List<OrderDetails>();
            Order? order = await _context.Orders!.FirstOrDefaultAsync(t => t.Id == orderId);
            Address? address = await _context.Addresses!.FirstOrDefaultAsync(t => t.UserId == order!.UserId);
            Provider? market = await _context.Providers!.FirstOrDefaultAsync(t => t.Id == order!.ProviderId);

            foreach (OrderItem item in orderItems)
            {
                List<ProductsOption> optionsList = new List<ProductsOption>();
                Product? product = await _context.Products!.FirstOrDefaultAsync(t => t.Id == item.ProductId);



                // if (item.Options != null)
                // {
                //     String[] options = item.Options!.Split("#");

                //     optionsList.Clear();
                //     foreach (var option in options)
                //     {
                //         ProductsOption? productsOption = await _context.ProductsOptions!.FirstOrDefaultAsync(x => x.Id == int.Parse(option));
                //         if (productsOption != null)
                //             optionsList.Add(productsOption!);
                //     }


                // }


                orderDetails.Add(new OrderDetails
                {
                    Product = product,
                    Order = item,
                    // Options = optionsList
                });

            }



            return new ResponseOrder
            {
                Products = orderDetails,
                provider = market,
                order = order,
                address = address
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


        // private async Task<AppConfig> getConfigKey(string key)
        // {
        //     AppConfig? appConfig = await _context.AppConfigs!.FirstOrDefaultAsync(x => x.Key == key);
        //     return appConfig!;
        // }

        public async Task<dynamic> GitOrdersByMarketId(int marketId)
        {
            List<Order> orders = await _context.Orders!.Where(t => t.ProviderId == marketId).ToListAsync();

            return new
            {
                successOrders = orders.Where(t => t.Status == 5).ToList(),
                unsuccessfulOrders = orders.Where(t => t.Status == -1).ToList(),
            };
        }

        public async Task<dynamic> GitOrdersByDriverId(string driverId, int AddressId)
        {
            List<ResponseOrder> currentOrders = new List<ResponseOrder>();
            List<ResponseOrder> successOrders = new List<ResponseOrder>();
            List<ResponseOrder> unSuccessOrders = new List<ResponseOrder>();

            // Address? userAddress = await _context.Addresses!.FirstOrDefaultAsync(x => x.Id == AddressId);
            List<Order> orders = await _context.Orders!.ToListAsync();


            return new
            {
                currentOrders = orders.Where(t => t.DriverId == driverId && t.Status == 0).ToList(),
                successOrders = orders.Where(t => t.DriverId == driverId && t.Status == 5).ToList(),
                unsuccessfulOrders = orders.Where(t => t.DriverId == driverId && t.Status == -1).ToList(),
            };
        }

        public async Task<dynamic> GetOrdersUser(string userId)
        {
            List<Order> orders = await _context.Orders!.Where(i => i.UserId == userId).ToListAsync();

            return orders;
        }



        // public async Task<dynamic> AcceptOrderDriver(int orderId, string driverId)
        // {
        //     Order? order = await _context.Orders!.FirstOrDefaultAsync(x => x.Id == orderId);
        //     if (order != null)
        //     {
        //         order.Status = 4;
        //         order.DriverId = driverId;
        //         User? driver = await _context.Users.FirstOrDefaultAsync(t => t.Id == driverId);
        //         if (driver != null)
        //         {
        //             driver.Status = "UnActive";
        //         }
        //     }
        //     await _context.SaveChangesAsync();

        //     return order!;
        // }


    }




}