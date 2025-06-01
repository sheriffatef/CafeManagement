using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using ServiceAbstraction;
using Shared.TablesDtos;

namespace Service
{
    public class TableService(IUnitOfWork _unitOfWork ,IMapper _mapper) : ITableService
    {
        public async Task CreateTableAsync(CreatedTableDto table)
        {
           await _unitOfWork.GetRepository<Tables, int>().AddAsync(_mapper.Map<Tables>(table));
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteTableAsync(int id)
        {
            var Repo =  _unitOfWork.GetRepository<Tables, int>();
            var table = await Repo.GetByIdAsync(id);
            if (table != null)
            {
                Repo.Delete(table);
                await _unitOfWork.SaveChangesAsync();
            }

        }

        public async Task<IEnumerable<TableDto>> GetAllTablesAsync()
        {
            var Repo =  _unitOfWork.GetRepository<Tables, int>();
            var Tables =await Repo.GetAllAsync();
            var TablesDto = _mapper.Map<IEnumerable<Tables>,IEnumerable<TableDto>>(Tables);
            return TablesDto;
        }

        public async Task<TableDto> GetTableByIdAsync(int id)
        {
            var Repo = await _unitOfWork.GetRepository<Tables, int>().GetByIdAsync(id);
             return Repo is not null ? _mapper.Map<TableDto>(Repo) : null;

        }

        public async Task UpdateTableAsync(int id, UpdatedTableDto table)
        {
            var Repo = await _unitOfWork.GetRepository<Tables, int>().GetByIdAsync(id);
            if (Repo != null)
            {
                Repo.TableName = table.TableName;
                Repo.Capacity = table.Capacity;
                _unitOfWork.GetRepository<Tables, int>().Update(Repo);
                await _unitOfWork.SaveChangesAsync();
            }


        }

        public async Task UpdateTableStatusAsync(int id, string status)
        {
            var Repo = await _unitOfWork.GetRepository<Tables, int>().GetByIdAsync(id);
            if (Repo != null)
            {
                Repo.Status = status;
                _unitOfWork.GetRepository<Tables, int>().Update(Repo);
                await _unitOfWork.SaveChangesAsync();
            }

        }
    }
}
