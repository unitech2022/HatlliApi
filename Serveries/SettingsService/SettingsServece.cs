using System.Text.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using HattliApi.Data;
using HattliApi.Models;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

using HattliApi.Models.BaseEntity;

using X.PagedList;
using HatlliApi.Models;

namespace HattliApi.Serveries.SettingsService
{
    public class SettingsService : ISettingsService

    {


        private readonly IMapper _mapper;

        private readonly IConfiguration _config;
        private readonly AppDBcontext _context;

        public SettingsService(IMapper mapper, IConfiguration config, AppDBcontext context)
        {
            _mapper = mapper;
            _config = config;
            _context = context;
        }

        public async Task<Setting> AddSetting(Setting setting)
        {

            await _context.Settings!.AddAsync(setting);

            await _context.SaveChangesAsync();

            return setting;

        }


        public async Task<BaseResponse> GetSettings(string UserId, int page)
        {
            List<Setting> categories = await _context.Settings!.ToListAsync();



            var pageResults = 10f;
            var pageCount = Math.Ceiling(categories.Count() / pageResults);

            var items = await categories
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

        public async Task<Setting> GitSettingById(int settingId)
        {

            Setting? setting = await _context.Settings!.FirstOrDefaultAsync(x => x.Id == settingId);
            return setting!;
        }

        public async  Task<Setting> UpdateSetting(string value, int settingId)
        {
            Setting? setting = await _context.Settings!.FirstOrDefaultAsync(t => t.Id == settingId);

            if (setting != null)
            {
                if (value != null)
                {
                    setting.Value = value;
                    await _context.SaveChangesAsync();
                }
               
            }
           return setting!;

        }

        public async Task<Setting> DeleteSetting(int settingId)
        {
            Setting? setting = await _context.Settings!.FirstOrDefaultAsync(x => x.Id == settingId);

            if (setting != null)
            {
                _context.Settings!.Remove(setting);

                await _context.SaveChangesAsync();
            }

            return setting!;
        }


        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public async Task<dynamic> GetAllSettings()
        {
            List<Setting> settings = await _context.Settings!.ToListAsync();
            return settings;
        }



    }
}