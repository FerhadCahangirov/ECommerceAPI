using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Persistence.Contexts;
using File = ECommerceAPI.Domain.Entities.File;

namespace ECommerceAPI.Persistence.Repositories
{
    public class FileWriteRepository : WriteRepository<File>, IFileWriteRepository
    {
        public FileWriteRepository(ECommerceAPIDbContext context) : base(context) { }
    }
}
