using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IServiceManager
    {
        public ITableService tableService { get; }
        public IProductService productService { get; }
        public IOrdersService OrderService { get; }
    }
}
