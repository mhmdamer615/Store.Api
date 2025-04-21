using AutoMapper;
using Domain.Contract;
using Domain.Entities;
using Services.Abstraction;
using Shared;
using Shared.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var mappedBrands = mapper.Map<IEnumerable<BrandResultDto>>(brands);
            return mappedBrands;
        }

        public async Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductSpecificationParams specifications)
        {
            var specs = new ProductWithFilterSpecification(specifications);
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(specs);
            var countSpec = new ProductWithFilterSpecification(specifications);
            var totalCount = await unitOfWork.GetRepository<Product, int>().CountAsync(countSpec);
            var mappedProducts = mapper.Map<IEnumerable<ProductResultDto>>(products);
            return new PaginatedResult<ProductResultDto>
              (specifications.PageIndex, specifications.PageSize, totalCount, mappedProducts);
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var mappedTypes = mapper.Map<IEnumerable<TypeResultDto>>(types);
            return mappedTypes;

        }

        public async Task<ProductResultDto> GetProductByIdAsync(int id)
        {
            var specs = new ProductWithFilterSpecification(id);
            var product = await unitOfWork.GetRepository<Product, int>().GetAsync(specs);
            var mappedProduct = mapper.Map<ProductResultDto>(product);
            return mappedProduct;

        }
    }
}
