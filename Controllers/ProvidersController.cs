using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using HattliApi.Models;

using HattliApi.Serveries.ProvidersService;
using Microsoft.AspNetCore.Authorization;

namespace HattliApi.Controllers
{
    [ApiController]
    [Route("provider")]
    public class ProvidersController : ControllerBase
    {
        private readonly IProvidersService _repository;
        private IMapper _mapper;
        public ProvidersController(IProvidersService repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }



        // [HttpGet]
        // [Route("get-marketDetails")]
        // public async Task<ActionResult> GetMarketDetails([FromQuery] int marketId,[FromQuery] string userId)
        // {




        //     return  Ok(await _repository.GetMarketDetails(marketId,userId));
        // }


        [HttpPost]
        [Route("delete-provider")]
        public async Task<ActionResult> DeleteProvider([FromForm] int marketId)
        {




            return Ok(await _repository.DeleteAsync(marketId));
        }



        // [HttpGet]
        // [Route("get-markets-byFieldId")]
        // public async Task<ActionResult> GetMarketsByFieldId([FromQuery] int fieldId,[FromQuery]int  AddressId,[FromQuery]int  page)
        // {




        //     return  Ok(await _repository.GetMarketsByFieldId(fieldId,AddressId,page));
        // }



        [HttpGet]
        [Route("get-providers-by-catId")]
        public async Task<ActionResult> GetGetProvidersByCategoryId([FromQuery] int categoryId, [FromQuery] string UserId)
        {

            return Ok(await _repository.GetProvidersByFieldId(categoryId, UserId));
        }

        [HttpPost]
        [Route("add-provider")]
        public async Task<ActionResult> AddMarket([FromForm] Provider provider)
        {
            if (provider == null)
            {
                return NotFound();
            }

            await _repository.AddAsync(provider);

            return Ok(provider);
        }


        [HttpPost]
        [Route("search-Provider")]
        public async Task<ActionResult> SearchMarket([FromForm] string textSearch, [FromForm] int addressId)
        {

            return Ok(await _repository.SearchProvider(textSearch, addressId));
        }

        [HttpGet]
        [Route("get-provider-details")]
        public async Task<ActionResult> DetailsProvider([FromQuery] int providerId, [FromQuery] string userId)
        {

            return Ok(await _repository.DetailsProvider(providerId, userId));
        }

        [HttpPost]
        [Route("update-provider")]
        public async Task<ActionResult> UpdateProvider([FromForm] Provider provider)
        {

            return Ok(await _repository.UpdateProvider(provider));
        }
        [HttpPost]
        [Route("change-phone-provider")]
        public async Task<ActionResult> ChangePhoneProvider([FromForm] string userId, [FromForm] string email, [FromForm] string password)
        {

            return Ok(await _repository.ChangePhoneProvider(email, password, userId));
        }

        [HttpPost]
        [Route("review-provider")]
        public async Task<ActionResult> ReviewProvider([FromForm] string userId, [FromForm] int to, [FromForm] string from)
        {

            return Ok(await _repository.ReviewProvider(userId, from, to));
        }
        // [Authorize(Roles = "provider")]
        [HttpPost]
        [Route("BalanceWithdrawal-provider")]
        public async Task<ActionResult> ReviewProvider([FromForm] string userId, [FromForm] double mony, [FromForm] int type)
        {

            return Ok(await _repository.BalanceWithdrawalProvider(userId, mony, type));
        }

    }
}