﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;

namespace Notes.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            
            services.AddDbContext<NotesDbContext>(b =>
            {
                b.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            services.AddScoped<INotesDbContext>(provider => 
                provider.GetService<NotesDbContext>()!);

            return services;
        }
    }
}