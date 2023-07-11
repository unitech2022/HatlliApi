using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using HattliApi.Dtos;
using HattliApi.Models;
using HattliApi.Serveries.ProductsService;

namespace HattliApi.Controllers
{

    // STATUS PRODUCT => بترجع الكمية بتاعت المنتج في السلة 

    [ApiController]
    [Route("product")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _repository;
        private IMapper _mapper;

        public ProductsController(IProductsService repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpPost]
        [Route("add-Product")]
        public async Task<ActionResult> AddProduct([FromForm] Product Product)
        {

            await _repository.AddProduct(Product);

            return Ok(Product);
        }


        [HttpGet]
        [Route("get-Products-By-providerId")]
        public async Task<ActionResult> GetProductsByProviderId([FromQuery] int providerId)
        {

            return Ok(await _repository.GetProductsByProviderId(providerId));
        }


        [HttpGet]
        [Route("get-Product-byId")]
        public async Task<ActionResult> GetProductById([FromQuery] int productId)
        {

            return Ok(await _repository.GitProductById(productId));
        }

        [HttpGet]
        [Route("get-Product-details")]
        public async Task<ActionResult> GetProductDetails([FromQuery] int productId, [FromQuery] string UserId)
        {

            return Ok(await _repository.GitProductDetails(productId, UserId));
        }

        [HttpGet]
        [Route("get-Products-By-Cate")]
        public async Task<ActionResult> GetProductsByCategoryId([FromQuery] string UserId, [FromQuery] int page, [FromQuery] int categoryId)
        {

            return Ok(await _repository.GitProductsByCategoryId(UserId, page, categoryId));
        }

        [HttpGet]
        [Route("search-products")]
        public async Task<ActionResult> SearchProducts([FromQuery] string UserId, [FromQuery] string textSearch,[FromQuery]  int type)
        {

            return Ok(await _repository.SearchProducts(UserId, textSearch,type));
        }

        [HttpPut]
        [Route("update-Product")]
        public async Task<ActionResult> UpdateProduct([FromForm] UpdateProductDto UpdateProduct, [FromForm] int id)

        {
            Product Product = await _repository.GitProductById(id);
            if (Product == null)
            {
                return NotFound();
            }
            _mapper.Map(UpdateProduct, Product);

            _repository.UpdateProduct(Product);
            _repository.SaveChanges();

            return Ok(Product);
        }

        [HttpPost]
        [Route("delete-Product")]
        public async Task<ActionResult> DeleteProduct([FromForm] int ProductId)
        {
            Product Product = await _repository.DeleteProduct(ProductId);

            if (Product == null)
            {

                return NotFound();
            }



            return Ok(Product);
        }



    }
}