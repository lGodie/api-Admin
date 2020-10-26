using System.Collections.Generic;
using System.Threading.Tasks;
using TestWork.Common.Models;
using TestWork.Common.Requests;
using TestWork.Common.Responses;
using TestWork.Domain.Services.Interface;
using TestWork.Infrastructure.Data.Entities;
using TestWork.Infrastructure.Data.Repositories.Interface;

namespace TestWork.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUsersRepository _usersRepository;

        public UserService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<Response> Delete(int id)
        {
            int result = await _usersRepository.Delete(id);

            if (result > 0)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = "Se elimino exitosamente",
                    Result = result

                };
            }
            else
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "No fue creada"
                };
            }
        }

        public async Task<Response> FindAll()
        {
            List<Users> result = await _usersRepository.FindAll();

            if (result.Count > 0)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = "Se creo exitosamente",
                    Result = result

                };
            }
            else
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "No fue creada"
                };
            }
        }

        public async Task<Response> Update(UserRequest data)
        {
            int result = await _usersRepository.Update(data);

            if (result > 0)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = "Se actualizó exitosamente",
                    Result = result

                };
            }
            else
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "No fue actualizada correctamente"
                };
            }
        }

        public async Task<Response> search(PaginationModel pagination)
        {
            List<Users> result = await _usersRepository.search(pagination);

            if (result.Count > 0)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = "Se consulto exitosamente",
                    Result = result

                };
            }
            else
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "No se encontraron registros"
                };
            }
        }
                
    }
}
