using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HatlliApi.ViewModels;
using HattliApi.Models;
using HattliApi.Models.BaseEntity;

namespace HattliApi.Serveries.ProductsService
{
    public interface IProductsService
    {

        Task<BaseResponse> GetProducts(string UserId, int page);

        Task<List<Product>> GetProductsByProviderId(int ProviderId);

        Task<BaseResponse> GitProductsByCategoryId(string UserId, int categoryId, int page);

 Task<BaseResponse> GitProductsByProviderIdPage( int providerId, int page);
        Task<Product> AddProduct(Product Product);

        Task<Product> GitProductById(int ProductId);

        Task<List<SearchResponse>> SearchProducts(string addressId,string textSearch,int type);


        
        Task<Product> GitProductDetails(int ProductId, string UserId);
        Task<Product> DeleteProduct(int ProductId);

        void UpdateProduct(Product Product);


        bool SaveChanges();
    }
}