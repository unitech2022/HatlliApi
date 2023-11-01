using System;

using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

using HattliApi.Data;
using HattliApi.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HatlliApi.Helpers
{
    public class Functions
    {
        public static double GetDistance(double Lat1,
                 double Long1, double Lat2, double Long2)
        {

            double dDistance = Double.MinValue;
            double dLat1InRad = Lat1 * (Math.PI / 180.0);
            double dLong1InRad = Long1 * (Math.PI / 180.0);
            double dLat2InRad = Lat2 * (Math.PI / 180.0);
            double dLong2InRad = Long2 * (Math.PI / 180.0);

            double dLongitude = dLong2InRad - dLong1InRad;
            double dLatitude = dLat2InRad - dLat1InRad;

            // Intermediate result a.
            double a = Math.Pow(Math.Sin(dLatitude / 2.0), 2.0) +
                       Math.Cos(dLat1InRad) * Math.Cos(dLat2InRad) *
                       Math.Pow(Math.Sin(dLongitude / 2.0), 2.0);

            // Intermediate result c (great circle distance in Radians).
            double c = 2.0 * Math.Asin(Math.Sqrt(a));

            // Distance.
            // const Double kEarthRadiusMiles = 3956.0;
            const Double kEarthRadiusKms = 6376.5;
            dDistance = kEarthRadiusKms * c;

            return dDistance;
        }


        //  public static async Task<ResponseOrder> getOrderDetails(int orderId, AppDBcontext _context)
        //     {
        //         List<OrderItem> orderItems = await _context.OrderItems!.Where(t => t.OrderId == orderId).ToListAsync();


        //         List<OrderDetails> orderDetails = new List<OrderDetails>();
        //         Order? order = await _context.Orders!.FirstOrDefaultAsync(t => t.Id == orderId);
        //         Console.WriteLine(order + "MARKET");

        //         Provider? market = await _context.Provider!.FirstOrDefaultAsync(t => t.Id == order!.RestaurantId);

        //         foreach (OrderItem item in orderItems)
        //         {

        //             List<ProductsOption> optionsList = new List<ProductsOption>();
        //             Product? product = await _context.Products!.FirstOrDefaultAsync(t => t.Id == item.ProductId);



        //             if (item.Options != null)
        //             {
        //                 String[] options = item.Options!.Split("#");

        //                 optionsList.Clear();
        //                 foreach (var option in options)
        //                 {
        //                     ProductsOption? productsOption = await _context.ProductsOptions!.FirstOrDefaultAsync(x => x.Id == int.Parse(option));
        //                     if (productsOption != null)
        //                         optionsList.Add(productsOption!);
        //                 }


        //             }


        //             orderDetails.Add(new OrderDetails
        //             {
        //                 Product = product,
        //                 // Order = item,
        //                 Options = optionsList
        //             });

        //         }


        //         return new ResponseOrder
        //         {
        //             Products = orderDetails,
        //             Market = market,
        //             order = order
        //         };
        //     }



        // string modle, string modleId,,NotificationData notificationData
        public static async Task<bool> SendNotificationAsync(AppDBcontext _context, string userId, int modelId, string title, string body, string image, string type
        )
        {

            User? user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            Alert alertFind = await _context.Alerts!.FirstOrDefaultAsync(x => x.PageId == modelId);
            if (alertFind != null)
            {

                alertFind.Description = body;
                alertFind.CreatedAt = DateTime.Now;
            }
            else
            {
                Alert alert = new Alert()
                {
                    Description = body,
                    title = title,
                    Type = type,
                    //     TitleEng=body,
                    //     BodyEng=title,
                    PageId = modelId,
                    //  //   Modle = modle ?? "user",
                    UserId = userId,
                    //     ModelId = "0000",
                    //     // Image = user.ProfileImage ?? "a.jpg",
                    //     IsRead = 1,

                };

                await _context.Alerts!.AddAsync(alert);
            }

            await _context.SaveChangesAsync();

            string token = user!.DeviceToken!;
            using (var client = new HttpClient())
            {
                var firebaseOptionsServerId = "AAAAMKkBAfk:APA91bHGEnqs9Ousu5Mu00NiXnhQetFQNbmwKPvuirkki8hHz_p50Q0IdRoTHoPvXwSrKfnQ6iX-wwjeDUoUy3jH_C4gxKmNXYQtjci4BPWQFI5ROdgbtWBqrM26ZehuIIS_0xomQq4M";

                client.BaseAddress = new Uri("https://fcm.googleapis.com");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization",
                    $"key={firebaseOptionsServerId}");
                var data = new
                {
                    to = token,
                    notification = new
                    {
                        body = body,
                        title = title,
                    },

                    click_action = "FLUTTER_NOTIFICATION_CLICK",
                    priority = "high",
                    modelId = modelId

                };


                var json = JsonConvert.SerializeObject(data);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var result = await client.PostAsync("/fcm/send", httpContent);

                return result.StatusCode.Equals(HttpStatusCode.OK);
            }
        }


        private static readonly HttpClient client = new HttpClient();

        
 public async Task sendSms(string code, string phone)
    {

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var data = new
            {
                userName = "hatli",
                numbers = phone,
                userSender = "hatli",
                apiKey = "804ecc4281295b4dbda0b8a01fdffce0",
                msg =  "رمز التحقق: "+ code
            };

            var json = JsonConvert.SerializeObject(data);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await client.PostAsync("https://www.msegat.com/gw/sendsms.php", httpContent);
            System.Diagnostics.Debug.WriteLine(result.Content.ReadAsStringAsync());
        }

    }

      

    }





}