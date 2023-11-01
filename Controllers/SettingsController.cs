
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using HattliApi.Serveries.CategoriesServices;
using HattliApi.Serveries.SettingsService;
using HatlliApi.Models;


namespace HattliApi.Controllers
{

    [Route("setting")]
    [ApiController]
    public class SettingsController : Controller
    {

        private readonly ISettingsService _repository;
        private IMapper _mapper;
        public SettingsController(ISettingsService repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }



        [HttpPost]
        [Route("add-setting")]
        public async Task<ActionResult> AddSetting([FromForm] Setting setting)
        {
            if (setting == null)
            {
                return NotFound();
            }

            await _repository.AddSetting(setting);

            return Ok(setting);
        }


        [HttpGet]
        [Route("get-settings")]
        public async Task<ActionResult> GetSettings()
        {

            return Ok(await _repository.GetAllSettings());
        }




        [HttpPost]
        [Route("update-setting")]
        public async Task<ActionResult> UpdateSetting([FromForm] string value, [FromForm] int settingId)

        {
            return Ok(await _repository.UpdateSetting(value, settingId));
        }

        [HttpPost]
        [Route("delete-category")]
        public async Task<ActionResult> DeleteSetting([FromForm] int settingId)
        {
            Setting setting = await _repository.DeleteSetting(settingId);

            if (setting == null)
            {

                return NotFound();
            }



            return Ok(setting);
        }

    }
}