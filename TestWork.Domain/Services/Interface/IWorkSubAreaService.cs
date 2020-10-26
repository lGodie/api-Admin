using System.Threading.Tasks;
using TestWork.Common.Requests;
using TestWork.Common.Responses;
using TestWork.Infrastructure.Data.Entities;

namespace TestWork.Domain.Services.Interface
{
    public interface IWorkSubAreaService
    {
        Task<Response> Create(SubAreaRequest model);
        Task<Response> FindAll();
        Task<Response> FindById(int id);
        Task<Response> Delete(int id);
        Task<Response> Update(SubAreaRequest request);
    }
}
