using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.ProductsDtos;

namespace ServiceAbstraction
{
    public interface IProductService
    {
        Task<IEnumerable<ProductsDto>> GetAllProductsAsync();
        Task<ProductsDto> GetProductByIdAsync(int id);
        Task CreateProductsAsync(CreatedProductsDto productsDto);
        Task UpdateProductAsync(int id, UpdatedProductDto productDto);

        Task DeleteProductAsync(int id);
        Task<IEnumerable<ProductsDto>> GetProductBycategoryAsync(string category);

    }
}
