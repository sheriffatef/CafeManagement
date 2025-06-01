using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using ServiceAbstraction;
using Shared.OrdersDtos;
using Shared.ProductsDtos;
using Shared.TablesDtos;

namespace Service
{
    public class OrderService(IUnitOfWork _unitOfWork, IMapper _mapper) : IOrdersService
    {
        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            // Get direct access to the DbContext for better performance
            var dbContext = _unitOfWork.GetDbContext();
            
            // Use a more optimized query with explicit projection
            var orders = await dbContext.Set<Orders>()
                .AsNoTracking() // Disable tracking for better performance
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    UserId = o.UserId ?? 0,
                    Status = o.Status,
                    total = o.TotalAmount,
                    OrderDate = o.OrderDate,
                    TableId = o.TableId,
                    GuestName = o.GuestName ?? string.Empty,
                    items = o.OrderItems.Select(oi => new OrderItemsDto
                    {
                        Id = oi.Id,
                        ProductId = oi.ProductId,
                        ProductName = oi.Product != null ? oi.Product.ProductName : string.Empty,
                        Quantity = oi.Quantity,
                        Price = oi.Price
                    }).ToList()
                })
                .AsSplitQuery() // Use split queries for better performance with complex includes
                .ToListAsync();
            
            return orders;
        }




        public async Task CreateOrderAsync(CreatedOrderDto dto)
        {
            var order = _mapper.Map<Orders>(dto);

            order.OrderDate = DateTime.UtcNow;
            order.Status = "new";
            decimal totalAmount = 0;

            foreach (var item in order.OrderItems)
            {
                var product = await _unitOfWork.GetRepository<Products, int>().GetByIdAsync(item.ProductId);
                if (product is null)
                    throw new Exception($"Product with ID {item.ProductId} not found");

                item.Price = product.Price;
                item.Product = product;
                totalAmount += product.Price * item.Quantity;
            }

            order.TotalAmount = totalAmount;

            await _unitOfWork.GetRepository<Orders, int>().AddAsync(order);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task CancleOrDeleteOrderAsync(int id)
        {
            var Repo = _unitOfWork.GetRepository<Orders, int>();
            var order = await Repo.GetByIdAsync(id);
            if (order != null)
            {
                Repo.Delete(order);
                await _unitOfWork.SaveChangesAsync();
            }

        }
        public async Task<OrderDto?> GetOrderByIdAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Orders, int>();

            var order = await repo.GetFirstOrDefaultAsync(
                predicate: o => o.Id == id,
                include: q => q.Include(o => o.OrderItems)
                                 .ThenInclude(oi => oi.Product)
            );

            if (order is null)
                return null;

            _ = order.OrderItems.Count;

            return _mapper.Map<OrderDto>(order);
        }




        public async Task<int> CreateGuestOrderAsync(CreateGuestOrderDto dto)
        {
            var order = new Orders
            {
                GuestName = dto.GuestName,
                TableId = dto.TableId,
                OrderDate = DateTime.UtcNow,
                Status = "new",
                OrderItems = new List<OrderItems>()
            };

            decimal total = 0;
            foreach (var item in dto.OrderItems)
            {
                var product = await _unitOfWork.GetRepository<Products, int>().GetByIdAsync(item.ProductId);
                if (product is null)
                    throw new Exception($"Product {item.ProductId} not found");

                order.OrderItems.Add(new OrderItems
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = product.Price
                });

                total += product.Price * item.Quantity;
            }

            order.TotalAmount = total;
            await _unitOfWork.GetRepository<Orders, int>().AddAsync(order);
            return order.Id;
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByTableIdAsync(int tableId)
        {
            // Get direct access to the DbContext through the repository
            var repo = _unitOfWork.GetRepository<Orders, int>();

            // Create a more optimized query that only selects the fields we need
            // Use a direct query with projection to minimize data transfer
            var dbContext = _unitOfWork.GetDbContext();

            // Use a more optimized query with explicit projection
            var orders = await dbContext.Set<Orders>()
                .AsNoTracking() // Disable tracking for better performance
                .Where(o => o.TableId == tableId)
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    UserId = o.UserId ?? 0,
                    Status = o.Status,
                    total = o.TotalAmount,
                    OrderDate = o.OrderDate,
                    TableId = o.TableId,
                    GuestName = o.GuestName ?? string.Empty,
                    items = o.OrderItems.Select(oi => new OrderItemsDto
                    {
                        Id = oi.Id,
                        ProductId = oi.ProductId,
                        ProductName = oi.Product != null ? oi.Product.ProductName : string.Empty,
                        Quantity = oi.Quantity,
                        Price = oi.Price
                    }).ToList()
                })
                .AsSplitQuery() // Use split queries for better performance with complex includes
                .ToListAsync();

            return orders;
        }
        public async Task<bool> UpdateOrderStatusAsync(int orderId, string newStatus)
        {
            var repo = _unitOfWork.GetRepository<Orders, int>();
            var order = await repo.GetByIdAsync(orderId);

            if (order is null)
                return false;

            order.Status = newStatus;
            repo.Update(order);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }


    }
}
