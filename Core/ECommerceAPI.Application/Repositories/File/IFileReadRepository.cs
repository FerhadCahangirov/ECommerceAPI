using ECommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = ECommerceAPI.Domain.Entities.File;

namespace ECommerceAPI.Application.Repositories
{
    public interface IFileReadRepository : IReadRepository<File>
    {
    }
}
