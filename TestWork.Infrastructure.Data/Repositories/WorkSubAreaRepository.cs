using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWork.Common.Dtos;
using TestWork.Common.Helpers;
using TestWork.Common.Requests;
using TestWork.Common.Responses;
using TestWork.Infrastructure.Data.Entities;
using TestWork.Infrastructure.Data.Repositories.Interface;

namespace TestWork.Infrastructure.Data.Repositories
{
    public class WorkSubAreaRepository : IWorkSubAreaRepository
    {

        private readonly string _connectionString;

        public WorkSubAreaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PC");
        }
        public async Task<int> Create(SubAreaRequest data)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_WorkSubArea_Create", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Name", data.Name));
                    cmd.Parameters.Add(new SqlParameter("@idArea", data.IdArea));
                    await sql.OpenAsync();
                    int resp = await cmd.ExecuteNonQueryAsync();
                    sql.Close();
                    return resp;
                }
            }
        }

        public async Task<List<WorkSubAreas>> FindAll()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_WorkSubAreas_FindAll", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    List<WorkSubAreas> response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader != null)
                        {
                            response = reader.Translate<WorkSubAreas>().ToList();
                        }
                    }

                    return response;
                }
            }
        }

        public async Task<WorkSubAreas> FindById(int id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_WorkSubArea_FindById", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", id));
                    WorkSubAreas response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader != null)
                        {
                            response = reader.Translate<WorkSubAreas>().FirstOrDefault();
                        }
                    }

                    return response;
                }
            }
        }

        public async Task<int> Delete(int id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[usp_WorkSubArea_Delete]", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", id));
                    await sql.OpenAsync();
                    int resp = await cmd.ExecuteNonQueryAsync();
                    sql.Close();
                    return resp;
                }
            }
        }

        public async Task<int> Update(SubAreaRequest data)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[usp_WorkSubArea_Update]", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Name", data.Name));
                    cmd.Parameters.Add(new SqlParameter("@Id", data.Id));
                    cmd.Parameters.Add(new SqlParameter("@idArea", data.IdArea));
                    await sql.OpenAsync();
                    int resp = await cmd.ExecuteNonQueryAsync();
                    sql.Close();
                    return resp;
                }
            }
        }

    
    }
}
