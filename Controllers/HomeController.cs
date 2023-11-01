using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HattliApi.Models;
using HattliApi.Serveries.HomeService;
using HattliApi.Serveries.ProvidersService;
using Microsoft.AspNetCore.Mvc;


namespace HatlliApi.Controllers
{
    [ApiController]
    [Route("home")]
    public class HomeController : ControllerBase
    {
         private readonly IHomeService _repository;
           private readonly IProvidersService _repositoryProvider;
        private IMapper _mapper;

        public HomeController(IHomeService repository, IMapper mapper,IProvidersService repositoryProvider)
        {
            _repository = repository;
             _repositoryProvider = repositoryProvider;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("get-home-data")]
        public async Task<ActionResult> GetFields([FromQuery] string UserId,[FromQuery] string  location)
        {

            return Ok(await _repository.GetHomeUserData(UserId,location));
        }


         [HttpGet]
        [Route("get-home-provider")]
        public async Task<ActionResult> GetHomeDataProvider([FromQuery] string UserId)
        {
            // Provider? provider =await _repositoryProvider.GitProviderByUserId(UserId);
            // if(provider ==null){
            //     return NotFound();
            // }

            return Ok(await _repository.GetHomeDataProvider(UserId));
        }

    }
}