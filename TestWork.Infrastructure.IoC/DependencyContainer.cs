using Microsoft.Extensions.DependencyInjection;
using TestWork.Domain.Services;
using TestWork.Domain.Services.Interface;
using TestWork.Infrastructure.Data.Repositories;
using TestWork.Infrastructure.Data.Repositories.Interface;

namespace TestWork.Infrastructure.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            #region Services

            services.AddScoped<IWorkAreaService, WorkAreaService>();
            services.AddScoped<IWorkSubAreaService, WorkSubAreaService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IIdentificationTypeService, IdentificationTypeService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            
            #endregion
            #region Repository

            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IWorkAreaRepository, WorkAreaRepository>();
            services.AddScoped<IWorkSubAreaRepository, WorkSubAreaRepository>();
            services.AddScoped<IIdentificationTypesRepository, IdentificationTypesRepository>();
            services.AddScoped<IUsersRepository, UserRepository>();
            #endregion
        }
    }
}
