using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HatlliApi.Serveries.AddressesServices;
using HatlliApi.Serveries.RateServices;
using HattliApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace HatlliApi.Controllers
{
    [Route("rates")]
    public class RatesController : Controller
    {

        private readonly IRateServices _repository;
        private IMapper _mapper;

        public RatesController(IRateServices repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("add-rate")]
        public async Task<ActionResult> AddRateMarket([FromForm] Rate rate)
        {

            return Ok(await _repository.AddRate(rate));

        }


        [HttpGet]
        [Route("rates-product")]
        public async Task<ActionResult> GetRatsProduct([FromQuery] int productId)
        {

            return Ok(await _repository.GetRatesByProductId(productId));

        }

    }
}