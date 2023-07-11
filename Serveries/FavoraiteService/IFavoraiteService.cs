using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HattliApi.Models;
using HattliApi.Models.BaseEntity;
using HatlliApi.Models;
using HatlliApi.ViewModels;

namespace HattliApi.Serveries.CategoriesServices
{
    public interface IFavoriteService
    {
        Task<List<FavoriteResponse>> GetFavorites(string UserId);


        Task<dynamic> GetAllFavorites();

        Task<Favorite> AddFavorite(Favorite category);

        Task<Favorite> GitFavoriteById(int CategoryId);


        Task<Favorite> DeleteFavorite(int CategoryId);

        void UpdateFavorite(Favorite category);


        bool SaveChanges();
    }
}