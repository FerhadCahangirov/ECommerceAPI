using ECommerceAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Repositories
{
    public interface IWriteRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<bool> AddAsync(TEntity model);
        Task<bool> AddRangeAsync(List<TEntity> models);
        bool Remove(TEntity model);
        bool RemoveRange(List<TEntity> models);
        Task<bool> RemoveAsync(string id);
        bool Update(TEntity model);
        Task<int> SaveAsync();

    }
}
