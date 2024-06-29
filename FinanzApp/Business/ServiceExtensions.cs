using Business.Interfaces;
using Business.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace Business
{
    public static class ServiceExtensions
    {
        public static void AddBusinessLayer(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();

        }
    }
}
