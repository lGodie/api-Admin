using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestWork.Common.Models;
using TestWork.Common.Requests;
using TestWork.Common.Responses;
using TestWork.Infrastructure.Data.Entities;

namespace TestWork.Domain.Services.Interface
{
    public interface IUserService
    {
        Task<Response> FindAll();
        Task<Response> Delete(int id);
        Task<Response> Update(UserRequest data);
        Task<Response> search(PaginationModel pagination);
    }
}
