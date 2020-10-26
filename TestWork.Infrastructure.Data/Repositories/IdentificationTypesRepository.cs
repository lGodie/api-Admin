using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TestWork.Common.Dtos;
using TestWork.Common.Helpers;
using TestWork.Common.Requests;
using TestWork.Common.Responses;
using TestWork.Infrastructure.Data.Entities;
using TestWork.Infrastructure.Data.Repositories.Interface;

namespace TestWork.Infrastructure.Data.Repositories
{
    public class IdentificationTypesRepository : IIdentificationTypesRepository
    {
        private readonly string _connectionString;

        public IdentificationTypesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PC");
        }

        public async Task<int> Create(IdentificationTypeRequest data)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_IdentificationTypes_Create", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Name", data.Name));
                    cmd.Parameters.Add(new SqlParameter("@Code", data.Code));
                    await sql.OpenAsync();
                    int resp = await cmd.ExecuteNonQueryAsync();
                    sql.Close();
                    return resp;
                }
            }
        }

        public async Task<List<IdentificationTypes>> FindAll()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_IdentificationTypes_FindAll", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    List<IdentificationTypes> response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader != null)
                        {
                            response = reader.Translate<IdentificationTypes>().ToList();
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
                using (SqlCommand cmd = new SqlCommand("[usp_IdentificationTypes_Delete]", sql))
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
        public async Task<int> Update(IdentificationTypeRequest data)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[usp_IdentificationTypes_Update]", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Name", data.Name));
                    cmd.Parameters.Add(new SqlParameter("@Code", data.Code));
                    cmd.Parameters.Add(new SqlParameter("@Id", data.Id));
                    await sql.OpenAsync();
                    int resp = await cmd.ExecuteNonQueryAsync();
                    sql.Close();
                    return resp;
                }
            }
        }

        public async Task<IdentificationTypes> FindById(int id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_IdentificationTypes_FindById", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", id));
                    IdentificationTypes response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader != null)
                        {
                            response = reader.Translate<IdentificationTypes>().FirstOrDefault();
                        }
                    }

                    return response;
                }
            }
        }

    }
}
