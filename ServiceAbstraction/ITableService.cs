using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.TablesDtos;

namespace ServiceAbstraction
{
    public interface ITableService
    {
        Task<IEnumerable<TableDto>> GetAllTablesAsync();
        Task<TableDto?> GetTableByIdAsync(int id);
        Task CreateTableAsync(CreatedTableDto table);
        Task UpdateTableAsync(int id,UpdatedTableDto table);
        Task UpdateTableStatusAsync(int id, string status);
        Task DeleteTableAsync(int id);

    }
}
