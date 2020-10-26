using System.Collections.Generic;
using System.Threading.Tasks;
using TestWork.Common.Dtos;
using TestWork.Common.Requests;
using TestWork.Infrastructure.Data.Entities;

namespace TestWork.Infrastructure.Data.Repositories.Interface
{
    public interface IIdentificationTypesRepository
    {
        Task<int> Create(IdentificationTypeRequest data);
        Task<List<IdentificationTypes>> FindAll();
        Task<IdentificationTypes> FindById(int id);
        Task<int> Delete(int id);
        Task<int> Update(IdentificationTypeRequest data);
    }
}
