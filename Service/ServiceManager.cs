using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using ServiceAbstraction;

namespace Service
{
    public class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper) : IServiceManager
    {
        private readonly Lazy<ITableService> _LazytableService = new Lazy<ITableService>(() => new TableService(unitOfWork, mapper));
        private readonly Lazy<IProductService> _LazyProductService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
        private readonly Lazy<IOrdersService> _LazyOrderService = new Lazy<IOrdersService>(() => new OrderService(unitOfWork, mapper));

        public virtual ITableService tableService => _LazytableService.Value;

        public virtual IProductService productService => _LazyProductService.Value;

        public virtual IOrdersService OrderService => _LazyOrderService.Value;

    }
}
