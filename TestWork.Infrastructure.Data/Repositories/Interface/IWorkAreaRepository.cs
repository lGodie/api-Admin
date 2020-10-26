using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestWork.Common.Dtos;
using TestWork.Common.Requests;
using TestWork.Infrastructure.Data.Entities;

namespace TestWork.Infrastructure.Data.Repositories.Interface
{
    public interface IWorkAreaRepository
    {
        Task<int> Create(AreaRequest data);
        Task<List<WorkAreas>> FindAll();
        Task<WorkAreas> FindById(int id);
        Task<int> Delete(int id);
        Task<int> Update(AreaRequest data);
    }
}
