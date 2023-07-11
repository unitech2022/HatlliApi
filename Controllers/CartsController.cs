using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;

using HatlliApi.Serveries.CartsService;
using HattliApi.Models;

namespace HatlliApi.Controllers
{

    [Authorize]
    [ApiController]
    [Route("cart")]
    public class CartsController : ControllerBase
    {

        private readonly ICartsService _repository;
        
        private IMapper _mapper;
        public CartsController(ICartsService repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }



        [HttpPost]
        [Route("add-cart")]
        public async Task<ActionResult> Addcart([FromForm] Cart cart)
        {
            if (cart == null)
            {
                return NotFound();
            }

            await _repository.AddCart(cart);

            return Ok(cart);
        }


        [HttpGet]
        [Route("get-carts")]
        public async Task<ActionResult> GetCarts([FromQuery] string UserId)
        {

            return Ok(await _repository.GetCarts(UserId));
        }




        [HttpPut]
        [Route("update-cart")]
        public async Task<ActionResult> UpdateCart([FromForm] int quantity,[FromForm] int id)

        {

             Cart cart = await _repository.GitCartById(id);

             if(cart ==null){
                return NotFound();
             }


            return Ok(await _repository.UpdateQuantityCart(quantity,id));

            
        }
        

        [HttpPost]
        [Route("delete-cart")]
        public async Task<ActionResult> DeleteCart([FromForm] int cartId)
        {
            Cart cart = await _repository.DeleteCart(cartId);

            if (cart == null)
            {

                return NotFound();
            }



            return Ok(cart);
        }



    }
}