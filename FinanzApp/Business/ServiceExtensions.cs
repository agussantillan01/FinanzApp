using Business.Interfaces;
using Business.Services;
using Infrastructure.CustomIdentity;
using Infrastructure.CustomIdentity.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace Business
{
    public static class ServiceExtensions
    {
        public static void AddBusinessLayer(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();

            //services.AddScoped<IUserRoleService, UserRoleService>

            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IActiveDirectoryManager, ActiveDirectoryManager>();
            services.AddTransient<IApplicationUserStore, ApplicationUserStore>();

        }
    }
}
