using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HattliApi.Dtos;
using HattliApi.Models;
using HattliApi.ViewModels;

namespace HattliApi.Profils
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserForRegister, User>();
            CreateMap<User, UserDetailResponse>();

            CreateMap<UpdateCategoryDto, Category>();
            // CreateMap<UpdateFieldDto, Field>();

            CreateMap<UpdateProductDto, Product>();
           CreateMap<UpdateCartDto, Cart>();
            //  CreateMap<UpdateOrderDto, Order>();
            //  CreateMap<Cart, Cart>();
            // CreateMap<UpdateOrderItemDto, OrderItem>();
            // CreateMap<UpdateProductOptionsDto, ProductsOption>();
            // CreateMap<UpdateOrderItemOptionDto, OrderItemOption>();
            // CreateMap<UpdateAddressDto, Address>();
            // CreateMap<UpdateAlertDto, Alert>();
            // CreateMap<UpdateAppConfigDto, AppConfig>();

        }
    }
}