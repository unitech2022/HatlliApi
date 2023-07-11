using System.Text.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using HattliApi.Data;
using HattliApi.Models;
using X.PagedList;
using HatlliApi.Models;
using HatlliApi.ViewModels;

namespace HattliApi.Serveries.CategoriesServices
{

    // order status 
    // 0 = > جديد 
     // 1 = > قيد التجهيز 
      // 2 = > قيد التوصيل 
       // 3 = > مستلم 
    public class FavoriteService : IFavoriteService

    {


        private readonly IMapper _mapper;

        private readonly IConfiguration _config;
        private readonly AppDBcontext _context;

        public FavoriteService(IMapper mapper, IConfiguration config, AppDBcontext context)
        {
            _mapper = mapper;
            _config = config;
            _context = context;
        }

        public async Task<Favorite> AddFavorite(Favorite favorite)
        {

            Favorite? favoriteNew = await _context.Favorites!.FirstOrDefaultAsync(t => t.ProductId == favorite.ProductId && t.UserId == favorite.UserId);
            if (favoriteNew != null)
            {
                _context.Favorites!.Remove(favoriteNew);
            }
            else
            {
                await _context.Favorites!.AddAsync(favorite);

            }



            await _context.SaveChangesAsync();

            return favorite;

        }


        public async Task<List<FavoriteResponse>> GetFavorites(string UserId)
        {
             List<FavoriteResponse> favorites=new List<FavoriteResponse>{};
            List<Favorite> allFavorites = await _context.Favorites!.Where(t => t.UserId == UserId).ToListAsync();

            foreach (var item in allFavorites)
            {
                Product? product=await _context.Products!.FirstOrDefaultAsync(t=>t.Id==item.ProductId);
                favorites.Add(new FavoriteResponse{
                    favorite=item,
                    product=product
                });
                
            }



            // var pageResults = 10f;
            // var pageCount = Math.Ceiling(categories.Count() / pageResults);

            // var items = await categories
            //     .Skip((page - 1) * (int)pageResults)
            //     .Take((int)pageResults)
            //     .ToListAsync();



            // BaseResponse baseResponse = new BaseResponse
            // {
            //     Items = items,
            //     CurrentPage = page,
            //     TotalPages = (int)pageCount
            // };

            return favorites;

        }

        public async Task<Favorite> GitFavoritesById(int FavoriteId)
        {

            Favorite? favorite = await _context.Favorites!.FirstOrDefaultAsync(x => x.Id == FavoriteId);
            return favorite!;
        }



        public async Task<Favorite> DeleteFavorite(int favoriteId)
        {
            Favorite? favorite = await _context.Favorites!.FirstOrDefaultAsync(x => x.Id == favoriteId);

            if (favorite != null)
            {
                _context.Favorites!.Remove(favorite);

                await _context.SaveChangesAsync();
            }

            return favorite!;
        }


        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public async Task<dynamic> GetAllCategories()
        {
            List<Category> categories = await _context.Categories!.ToListAsync();
            return categories;
        }

        public Task<dynamic> GetAllFavorites()
        {
            throw new NotImplementedException();
        }

        public Task<Favorite> GitFavoriteById(int CategoryId)
        {
            throw new NotImplementedException();
        }

        public void UpdateFavorite(Favorite category)
        {
            throw new NotImplementedException();
        }
    }
}