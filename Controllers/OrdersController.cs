using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using AutoMapper;
using HatlliApi.Serveries.OrdersServices;
using HattliApi.Models;
using HattliApi.Dtos;
using HatlliApi.Models;

namespace HatlliApi.Controllers
{


    // order status
    // 0 في انتظار التأكيد
    // 1 تم تأكيد طلبك
    // 2 جارى التجهيز 
    // 3 تم التجهيز 
    // 4 جارى التوصيل 
    // 5 تم التسليم 
    [Authorize]
    [ApiController]
    [Route("orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersServices _repository;
        private IMapper _mapper;
        public OrdersController(IOrdersServices repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }



        [HttpPost]
        [Route("add-order")]
        public async Task<ActionResult> AddOrder([FromForm] string userId, [FromForm] int payment, [FromForm] string nots)
        {

            // User user = await _repository.GetUser(userId);
            // if (user.Status == 1)
            // {
            //     return Unauthorized();
            // }

            var order = await _repository.AddOrder(userId, payment, nots);

            return Ok(order);
        }
        [HttpPost]
        [Route("add-manual-order")]
        public async Task<ActionResult> AddOrder([FromForm] ManualOrder manualOrder)
        {


            var order = await _repository.AddManualOrder(manualOrder);

            return Ok(order);
        }

        [HttpGet]
        [Route("get-Orders")]
        public async Task<ActionResult> GetOrders([FromQuery] string UserId)
        {

            return Ok(await _repository.GetOrdersUser(UserId));
        }

        [HttpGet]
        [Route("get-Orders-by-marketId")]
        public async Task<ActionResult> GetOrdersByMarjetId([FromQuery] int marketId)
        {

            return Ok(await _repository.GitOrdersByMarketId(marketId));
        }


        [HttpGet]
        [Route("get-Orders-by-driverId")]
        public async Task<ActionResult> GetOrdersByDriverId([FromQuery] string driverId, [FromQuery] int addressId)
        {

            return Ok(await _repository.GitOrdersByDriverId(driverId, addressId));
        }


        [HttpGet]
        [Route("get-OrderDetails")]
        public async Task<ActionResult> GetOrderDetails([FromQuery] int orderId)
        {

            return Ok(await _repository.GitOrderDetails(orderId));
        }





        [HttpPut]
        [Route("update-Order")]
        public async Task<ActionResult> UpdateOrder([FromForm] UpdateOrderDto UpdateOrder, [FromForm] int id)

        {
            Order Order = await _repository.GitById(id);
            if (Order == null)
            {
                return NotFound();
            }
            _mapper.Map(UpdateOrder, Order);

            // _repository.UpdateObject(Order);
            _repository.SaveChanges();

            return Ok(Order);
        }



        [HttpPut]
        [Route("update-Order-status")]
        public async Task<ActionResult> UpdateOrderStatus([FromForm] int status, [FromForm] int orderId, [FromForm] int sender)

        {


            return Ok(await _repository.UpdateOrderStatus(orderId, status, sender));

        }

        [HttpPut]
        [Route("confirm-manual-Order")]
        public async Task<ActionResult> ConfirmManuaOrderStatus([FromForm] double price, [FromForm] int orderId)

        {


            return Ok(await _repository.ConfirmManualOrderStatus(orderId, price));

        }


        [HttpPut]
        [Route("payment-Order")]
        public async Task<ActionResult> PaymentOrder([FromForm] int orderId, [FromForm] int payment)

        {


            return Ok(await _repository.PaymentOrder(orderId, payment));

        }

        // [HttpPut]
        // [Route("accept-order-driver")]
        // public async Task<ActionResult> AcceptOrderDriver([FromForm] string driverId, [FromForm] int orderId)

        // {


        //     return Ok(await _repository.AcceptOrderDriver(orderId, driverId));

        // }



        [HttpPost]
        [Route("delete-Order")]
        public async Task<ActionResult> DeleteOrder([FromForm] int OrderId)
        {
            Order Order = await _repository.DeleteAsync(OrderId);

            if (Order == null)
            {

                return NotFound();
            }



            return Ok(Order);
        }

    }
}