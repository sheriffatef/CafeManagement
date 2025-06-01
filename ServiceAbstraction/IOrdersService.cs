using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.OrdersDtos;
using Shared.ProductsDtos;

namespace ServiceAbstraction
{
    public interface IOrdersService
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task CreateOrderAsync(CreatedOrderDto ordersDto);
        Task CancleOrDeleteOrderAsync(int id);

        Task<OrderDto> GetOrderByIdAsync(int id);
        Task<int> CreateGuestOrderAsync(CreateGuestOrderDto dto);
        Task<IEnumerable<OrderDto>> GetOrdersByTableIdAsync(int tableId);
        Task<bool> UpdateOrderStatusAsync(int orderId, string newStatus);


        //Task UpdateProductAsync(int id, UpdatedProductDto productDto);

        //Task DeleteProductAsync(int id);
        //Task<IEnumerable<ProductsDto>> GetProductBycategoryAsync(string category);

    }
}
