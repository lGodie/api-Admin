using System.Threading.Tasks;
using TestWork.Common.Requests;
using TestWork.Common.Responses;
using TestWork.Infrastructure.Data.Entities;

namespace TestWork.Domain.Services.Interface
{
    public interface IRoleService
    {
        Task<Response> Create(RoleRequest model);
        Task<Response> FindAll();
        Task<Response> FindById(int id);
        Task<Response> Delete(int id);
        Task<Response> Update(RoleRequest request);
    }
}
