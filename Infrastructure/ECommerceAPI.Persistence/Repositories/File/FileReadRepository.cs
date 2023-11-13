using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Persistence.Contexts;
using File = ECommerceAPI.Domain.Entities.File;

namespace ECommerceAPI.Persistence.Repositories
{
    public class FileReadRepository : ReadRepository<File>, IFileReadRepository
    {
        public FileReadRepository(ECommerceAPIDbContext context) : base(context)
        {
        }
    }
}
