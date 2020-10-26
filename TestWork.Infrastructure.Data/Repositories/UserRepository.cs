using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using TestWork.Common.Helpers;
using TestWork.Common.Models;
using TestWork.Common.Requests;
using TestWork.Common.Responses;
using TestWork.Infrastructure.Data.Entities;
using TestWork.Infrastructure.Data.Repositories.Interface;

namespace TestWork.Infrastructure.Data.Repositories
{
    public class UserRepository : IUsersRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PC");
        }
        public async Task<int> CreateUser(Users data)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_Users_Create", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@username", data.Username));
                    cmd.Parameters.Add(new SqlParameter("@name", data.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@password", data.Password));
                    cmd.Parameters.Add(new SqlParameter("@email",data.Email ));
                    cmd.Parameters.Add(new SqlParameter("@document", data.Document));
                    cmd.Parameters.Add(new SqlParameter("@firstName",data.FirstName ));
                    cmd.Parameters.Add(new SqlParameter("@lastName", data.LastName));
                    cmd.Parameters.Add(new SqlParameter("@idRole", data.IdRole));
                    cmd.Parameters.Add(new SqlParameter("@idIdentificationType",data.IdIdentificationType));
                    cmd.Parameters.Add(new SqlParameter("@idWorkSubArea", data.IdWorkSubArea));
                    await sql.OpenAsync();
                   int resp= await cmd.ExecuteNonQueryAsync();
                    sql.Close();
                    return resp;
                }
            }
        }

        public async Task<bool> Login(TokenRequest user)
        {
            bool response = false;

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_Users_Login", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Email", user.Email));
                    cmd.Parameters.Add(new SqlParameter("@Password", user.Password));
                    await sql.OpenAsync();
                    response = (bool)cmd.ExecuteScalar();
                    cmd.Connection.Close();
                    return response;
                }
            }
        }


        public async Task<UserResponse> FindById(int id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_Users_SelectById", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", id));
                    UserResponse response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToValue(reader);
                        }
                    }

                    return response;
                }
            }
        }

        private UserResponse MapToValue(SqlDataReader reader)
        {
            return new UserResponse()
            {
                //Id = (int)reader["Id"],
                FirstName = reader["firstName"].ToString(),
                Name = reader["Name"].ToString(),
                Email = reader["email"].ToString(),
                LastName = reader["lastName"].ToString(),
                Document = reader["document"].ToString(),
                WorkSubArea = reader["SubArea"].ToString(),
                Role = reader["role"].ToString(),
                IdentificationTypes = reader["NameidentificationType"].ToString()
            };
        }

        public async Task<UserResponse> FindByEmail(string email)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_Users_SelectByEmail", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@email", email));
                    UserResponse response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToValue(reader);
                        }
                    }

                    return response;
                }
            }
        }


        public async Task<List<Users>> FindAll()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_Users_FindAll", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    List<Users> response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader != null)
                        {
                            response = reader.Translate<Users>().ToList();
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
                using (SqlCommand cmd = new SqlCommand("[usp_Users_Delete]", sql))
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
        public async Task<int> Update(UserRequest data)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[usp_Users_Update]", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@username", data.Username));
                    cmd.Parameters.Add(new SqlParameter("@name", data.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@password", data.Password));
                    cmd.Parameters.Add(new SqlParameter("@email", data.Email));
                    cmd.Parameters.Add(new SqlParameter("@document", data.Document));
                    cmd.Parameters.Add(new SqlParameter("@firstName", data.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@lastName", data.LastName));
                    cmd.Parameters.Add(new SqlParameter("@idRole", data.IdRole));
                    cmd.Parameters.Add(new SqlParameter("@idIdentificationType", data.IdIdentificationTypes));
                    cmd.Parameters.Add(new SqlParameter("@idWorkSubArea", data.IdWorkSubArea));
                    await sql.OpenAsync();
                    int resp = await cmd.ExecuteNonQueryAsync();
                    sql.Close();
                    return resp;
                }
            }
        }

        public async Task<List<Users>> search(PaginationModel pagination)
        {

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_Users_Search", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ActualPage", pagination.ActualPage));
                    cmd.Parameters.Add(new SqlParameter("@PageSize", pagination.PageSize));
                    cmd.Parameters.Add(new SqlParameter("@searchString", pagination.searchString));
                    cmd.Parameters.Add(new SqlParameter("@PageQuatity", pagination.PageQuantity));
                    cmd.Parameters.Add(new SqlParameter("@idrole", pagination.idrole));

                    List<Users> response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader != null)
                        {
                            response = reader.Translate<Users>().ToList();
                        }
                    }

                    return response;
                }
            }

        }

    }
}
