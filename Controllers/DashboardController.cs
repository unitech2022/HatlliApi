using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HatlliApi.Serveries.DashboardServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HatlliApi.Controllers
{
    [Route("dashboard")]
    public class DashboardController : Controller
    {

        private readonly IDashboardServices _repository;
        private IMapper _mapper;
        public DashboardController(IDashboardServices repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }




        [HttpGet]
        [Route("home")]
        public async Task<ActionResult> GetHomeDashboard([FromQuery] string userId)
        {
            return Ok(await _repository.GetHomeDashboard(userId));
        }

        [HttpPost]
        [Route("get-providers")]
        public async Task<ActionResult> GetProviders([FromForm] int page, [FromForm] string textSearch)
        {
            return Ok(await _repository.GetProviders(page, textSearch));
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("update-provider")]
        public async Task<ActionResult> UpdateProviderStatus([FromForm] int providerId, [FromForm] int status)
        {
            return Ok(await _repository.UpdateStatusProvider(providerId, status));
        }



        [HttpPost]
        [Route("get-products")]
        public async Task<ActionResult> GetProducts([FromForm] int page, [FromForm] string textSearch)
        {
            return Ok(await _repository.GetProducts(page, textSearch));
        }


        [HttpPost]
        [Route("get-users")]
        public async Task<ActionResult> GetUsers([FromForm] int page, [FromForm] string textSearch)
        {
            return Ok(await _repository.GetUsers(page, textSearch));
        }

        [HttpPost]
        [Route("get-orders")]
        public async Task<ActionResult> GetOrders([FromForm] int page)
        {
            return Ok(await _repository.GetOrders(page));
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("update-product-status")]
        public async Task<ActionResult> UpdateProductStatus([FromForm] int productId, [FromForm] int status)
        {
            return Ok(await _repository.UpdateStatusProduct(productId, status));
        }
    }
}