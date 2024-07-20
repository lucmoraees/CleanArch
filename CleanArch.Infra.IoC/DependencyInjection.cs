using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CleanArch.Application.Interfaces;
using CleanArch.Application.Services;
using CleanArch.Infra.Data.Repositories;
using CleanArch.Domain.Interfaces;
using CleanArch.Infra.Data.Context;
using CleanArch.Application.Mappings;
using CleanArch.Infra.Data.Identity;
using Microsoft.AspNetCore.Identity;
using CleanArch.Domain.Account;

namespace CleanArch.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Conexão com o banco de dados
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            // Identiy
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
                options.AccessDeniedPath = "/Account/Login");

            // Authentication
            services.AddScoped<IAuthenticate, AuthenticateService>();
            services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();

            // Repositórios
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            // Services
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();

            // Domain
            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            // Mediator
            var myhandlers = AppDomain.CurrentDomain.Load("CleanArch.Application");
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(myhandlers));

            return services;
        }
    }
}
