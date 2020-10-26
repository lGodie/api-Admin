using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TestWork.Common.Dtos;
using TestWork.Common.Helpers;
using TestWork.Common.Requests;
using TestWork.Infrastructure.Data.Entities;
using TestWork.Infrastructure.Data.Repositories.Interface;

namespace TestWork.Infrastructure.Data.Repositories
{
    public class WorkAreaRepository : IWorkAreaRepository
    {
        private readonly string _connectionString;

        public WorkAreaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PC");
        }
        public async Task<int> Create(AreaRequest data)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_WorkArea_Create", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Name", data.Name));
                    await sql.OpenAsync();
                    int resp = await cmd.ExecuteNonQueryAsync();
                    sql.Close();
                    return resp;
                }
            }
        }

        public async Task<List<WorkAreas>> FindAll()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_WorkAreas_FindAll", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    List<WorkAreas> response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader != null)
                        {
                            response = reader.Translate<WorkAreas>().ToList();
                        }
                    }

                    return response;
                }
            }
        }

        public async Task<WorkAreas> FindById(int id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_WorkSubArea_FindById", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", id));
                    WorkAreas response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader != null)
                        {
                            response = reader.Translate<WorkAreas>().FirstOrDefault();
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
                using (SqlCommand cmd = new SqlCommand("[usp_WorkArea_Delete]", sql))
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

        public async Task<int> Update(AreaRequest data)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[usp_WorkArea_Update]", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Name", data.Name));
                    cmd.Parameters.Add(new SqlParameter("@Id", data.Id));
                    await sql.OpenAsync();
                    int resp = await cmd.ExecuteNonQueryAsync();
                    sql.Close();
                    return resp;
                }
            }
        }

    }

}

