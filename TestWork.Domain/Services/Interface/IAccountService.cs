using System.Threading.Tasks;
using TestWork.Common.Models;
using TestWork.Common.Requests;
using TestWork.Common.Responses;

namespace TestWork.Domain.Services.Interface
{
    public interface IAccountService
    {
        Task<Response> CreateUser(UserRequest SearchString);
        Task<Response> Login(TokenRequest SearchString);
    }
}
