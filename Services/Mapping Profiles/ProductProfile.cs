﻿using AutoMapper;
using Domain.Entities;
using Shared.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mapping_Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResultDto>()
                .ForMember(d => d.BrandName, opt => opt.MapFrom(src => src.ProductBrand.Name))
                .ForMember(d => d.TypeName, opt => opt.MapFrom(src => src.ProductType.Name))
                .ForMember(d => d.PictureUrl, opt => opt.MapFrom<PictureUrlResolver>());




            CreateMap<ProductType, TypeResultDto>();
            CreateMap<ProductBrand, BrandResultDto>();

        }
    }
}
