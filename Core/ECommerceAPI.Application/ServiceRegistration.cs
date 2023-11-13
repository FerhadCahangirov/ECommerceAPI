using ECommerceAPI.Application.Features.Commands.Product.CreateProduct;
using ECommerceAPI.Application.Features.Queries.Product.GetAllProduct;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection collection)
        {
            collection.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(typeof(ServiceRegistration).Assembly, typeof(GetAllProductQueryHandler).Assembly);
                cfg.RegisterServicesFromAssemblies(typeof(ServiceRegistration).Assembly, typeof(CreateProductCommandHandler).Assembly);

            });
        }
    }
}
