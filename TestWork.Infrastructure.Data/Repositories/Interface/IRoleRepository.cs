using System.Collections.Generic;
using System.Threading.Tasks;
using TestWork.Common.Dtos;
using TestWork.Common.Requests;
using TestWork.Common.Responses;
using TestWork.Infrastructure.Data.Entities;

namespace TestWork.Infrastructure.Data.Repositories.Interface
{
    public interface IRoleRepository
    {
        Task<int> Create(RoleRequest data);
        Task<List<Roles>> FindAll();
        Task<Roles> FindById(int id);
        Task<int> Delete(int id);
        Task<int> Update(RoleRequest data);
    }
}
