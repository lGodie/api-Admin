using System.Collections.Generic;
using System.Threading.Tasks;
using TestWork.Common.Requests;
using TestWork.Common.Responses;
using TestWork.Domain.Services.Interface;
using TestWork.Infrastructure.Data.Entities;
using TestWork.Infrastructure.Data.Repositories.Interface;

namespace TestWork.Domain.Services
{
    public class WorkSubAreaService : IWorkSubAreaService
    {
        private readonly IWorkSubAreaRepository _workSubAreaRepository;

        public WorkSubAreaService(IWorkSubAreaRepository workSubAreaRepository)
        {
            _workSubAreaRepository = workSubAreaRepository;
        }
        public async Task<Response> Create(SubAreaRequest model)
        {

            int result = await _workSubAreaRepository.Create(model);

            if (result == 1)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = "Se creo exitosamente"

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
            List<WorkSubAreas> result = await _workSubAreaRepository.FindAll();

            if (result.Count > 0)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = "Se creo exitosamente",
                    Result= result

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

        public async Task<Response> FindById(int id)
        {
            WorkSubAreas result = await _workSubAreaRepository.FindById(id);

            if (result.Id > 0)
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
                    Message = "Error"
                };
            }
        }

        public async Task<Response> Delete(int id)
        {
            int result = await _workSubAreaRepository.Delete(id);

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

        public async Task<Response> Update(SubAreaRequest request)
        {
            int result = await _workSubAreaRepository.Update(request);

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

    }
}
