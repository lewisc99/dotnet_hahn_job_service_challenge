﻿using Application.Interfaces;
using Domain.Database;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Pagination;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services;

namespace Infrastructure.Ioc
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureAppServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), builder =>
                        builder.MigrationsAssembly("Domain")));

            services.AddScoped<IRepositoryPagination<Breed>, Repository<Breed>>();
            services.AddScoped<IBreedPaginationService, BreedsService>();

            return services;
        }
    }
}
