using System.Collections.Generic;
using System.Threading.Tasks;
using TestWork.Common.Dtos;
using TestWork.Common.Requests;
using TestWork.Infrastructure.Data.Entities;

namespace TestWork.Infrastructure.Data.Repositories.Interface
{
    public interface IWorkSubAreaRepository
    {
        Task<int> Create(SubAreaRequest data);
        Task<List<WorkSubAreas>> FindAll();
        Task<WorkSubAreas> FindById(int id);
        Task<int> Delete(int id);
        Task<int> Update(SubAreaRequest data);
    }
}
