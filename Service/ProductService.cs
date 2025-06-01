using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using ServiceAbstraction;
using Shared.ProductsDtos;
using Shared.TablesDtos;

namespace Service
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task CreateProductsAsync(CreatedProductsDto productsDto)
        {
            await _unitOfWork.GetRepository<Products, int>().AddAsync(_mapper.Map<Products>(productsDto));
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductsDto>> GetAllProductsAsync()
        {
            var Repo = _unitOfWork.GetRepository<Products, int>();
            var Products = await Repo.GetAllAsync();
            var ProductsDto = _mapper.Map<IEnumerable<Products>, IEnumerable<ProductsDto>>(Products);
            return ProductsDto;
        }
        public async Task<ProductsDto> GetProductByIdAsync(int id)
        {
            var Repo = await _unitOfWork.GetRepository<Products, int>().GetByIdAsync(id);
            return Repo is not null ? _mapper.Map<ProductsDto>(Repo) : null;

        }

        public async Task UpdateProductAsync(int id, UpdatedProductDto productDto)
        {
            var Repo = await _unitOfWork.GetRepository<Products, int>().GetByIdAsync(id);
            if (Repo != null)
            {
                Repo.ProductName = productDto.ProductName;
                Repo.Description = productDto.Description;
                Repo.Price = productDto.Price;
                Repo.Category = productDto.Category;
                Repo.ImageUrl = productDto.ImageUrl;


                _unitOfWork.GetRepository<Products, int>().Update(Repo);
                await _unitOfWork.SaveChangesAsync();
            }


        }
        public async Task DeleteProductAsync(int id)
        {
            var Repo = _unitOfWork.GetRepository<Products, int>();
            var product = await Repo.GetByIdAsync(id);
            if (product != null)
            {
                Repo.Delete(product);
                await _unitOfWork.SaveChangesAsync();
            }

        }

        public async Task<IEnumerable<ProductsDto>> GetProductBycategoryAsync(string category)
        {
            var repo = _unitOfWork.GetRepository<Products, int>();
            var products = (await repo.GetAllAsync()).Where(p => p.Category == category).ToList();

            if (products.Any())
            {
                var productsDto = _mapper.Map<IEnumerable<Products>, IEnumerable<ProductsDto>>(products);
                return productsDto;
            }

            throw new Exception("No products found in this category");
        }

    }

}
