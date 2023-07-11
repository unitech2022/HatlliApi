using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HattliApi.Dtos;
using HattliApi.Models;
using HattliApi.Serveries.CategoriesServices;
using HatlliApi.Models;

namespace HattliApi.Controllers
{
    [Route("fav")]
    [ApiController]
    public class FavoritesController : Controller
    {

        private readonly IFavoriteService _repository;
        private IMapper _mapper;
        public FavoritesController(IFavoriteService repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }



        [HttpPost]
        [Route("add-favorite")]
        public async Task<ActionResult> AddFavorite([FromForm] Favorite favorite)
        {
            if (favorite == null)
            {
                return NotFound();
            }

            await _repository.AddFavorite(favorite);

            return Ok(favorite);
        }


        [HttpGet]
        [Route("get-favorites")]
        public async Task<ActionResult> GetFavorites([FromQuery] string UserId)
        {

            return Ok(await _repository.GetFavorites(UserId));
        }




        //  [HttpPut]
        // [Route("update-favorite")]
        // public async Task<ActionResult> UpdateFavorite([FromForm] UpdateCategoryDto UpdateCategory, [FromForm] int id)

        // {
        //     Category category = await _repository.GitCategoryById(id);
        //     if (category == null)
        //     {
        //         return NotFound();
        //     }
        //     _mapper.Map(UpdateCategory, category);

        //     _repository.UpdateCategory(category);
        //     _repository.SaveChanges();

        //     return Ok(category);
        // }

        [HttpPost]
        [Route("delete-favorite")]
        public async Task<ActionResult> DeleteFavorite([FromForm] int favoriteId)
        {
            Favorite favorite = await _repository.DeleteFavorite(favoriteId);

            if (favorite == null)
            {

                return NotFound();
            }



            return Ok(favorite);
        }

    }
}