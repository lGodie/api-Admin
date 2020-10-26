using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestWork.Common.Models;
using TestWork.Common.Requests;
using TestWork.Common.Responses;
using TestWork.Infrastructure.Data.Entities;

namespace TestWork.Infrastructure.Data.Repositories.Interface
{
    public interface IUsersRepository
    {
        Task<int> CreateUser(Users data);
        Task<bool> Login(TokenRequest data);
        Task<UserResponse> FindById(int id);
        Task<UserResponse> FindByEmail(string id);
        Task<List<Users>> FindAll();
        Task<int> Delete(int id);
        Task<int> Update(UserRequest data);
        Task<List<Users>> search(PaginationModel pagination);
    }
}
