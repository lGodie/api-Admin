using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestWork.Common.Requests;
using TestWork.Common.Responses;

namespace TestWork.Domain.Services.Interface
{
    public interface IWorkAreaService
    {
        Task<Response> Create(AreaRequest model);
        Task<Response> FindAll();
        Task<Response> FindById(int id);
        Task<Response> Delete(int id);
        Task<Response> Update(AreaRequest request);
    }
}
