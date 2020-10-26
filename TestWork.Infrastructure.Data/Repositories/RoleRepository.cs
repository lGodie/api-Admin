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
    public class RoleRepository :  IRoleRepository
    {
        private readonly string _connectionString;

        public RoleRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PC");
        }

        public async Task<int> Create(RoleRequest data)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_Role_Create", sql))
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

        public async Task<List<Roles>> FindAll()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_Role_FindAll", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    List<Roles> response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader != null)
                        {
                            response = reader.Translate<Roles>().ToList();
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
                using (SqlCommand cmd = new SqlCommand("[usp_Role_Delete]", sql))
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
        public async Task<int> Update(RoleRequest data)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[usp_Role_Update]", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Name", data.Name));
                    cmd.Parameters.Add(new SqlParameter("@Active", data.Active));
                    cmd.Parameters.Add(new SqlParameter("@Id", data.Id));
                    await sql.OpenAsync();
                    int resp = await cmd.ExecuteNonQueryAsync();
                    sql.Close();
                    return resp;
                }
            }
        }

        public async Task<Roles> FindById(int id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_Role_FindById", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", id));
                    Roles response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader != null)
                        {
                            response = reader.Translate<Roles>().FirstOrDefault();
                        }
                    }

                    return response;
                }
            }
        }

    }
}
