using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HattliApi.Models;
using HattliApi.Models.BaseEntity;

namespace HattliApi.Serveries.CategoriesServices
{
    public interface ICategoriesService
    {
        Task<BaseResponse> GetCategories(string UserId,int page);


 Task<dynamic> GetAllCategories();

        Task<Category> AddCategory(Category category);

         Task<Category> GitCategoryById(int CategoryId);


        Task<Category> DeleteCategory(int CategoryId);

        void UpdateCategory(Category category);


         bool SaveChanges();
    }
}