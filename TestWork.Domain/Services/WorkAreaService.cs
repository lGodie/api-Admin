using System.Collections.Generic;
using System.Threading.Tasks;
using TestWork.Common.Requests;
using TestWork.Common.Responses;
using TestWork.Domain.Services.Interface;
using TestWork.Infrastructure.Data.Entities;
using TestWork.Infrastructure.Data.Repositories.Interface;

namespace TestWork.Domain.Services
{
    public class WorkAreaService : IWorkAreaService
    {
        private readonly IWorkAreaRepository _workAreaRepository;

        public WorkAreaService(IWorkAreaRepository workAreaRepository)
        {
            _workAreaRepository = workAreaRepository;
        }
        public async Task<Response> Create(AreaRequest model)
        {

            int result = await _workAreaRepository.Create(model);

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
            List<WorkAreas> result = await _workAreaRepository.FindAll();

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
            WorkAreas result = await _workAreaRepository.FindById(id);

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
            int result = await _workAreaRepository.Delete(id);

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

        public async Task<Response> Update(AreaRequest request)
        {
            int result = await _workAreaRepository.Update(request);

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
