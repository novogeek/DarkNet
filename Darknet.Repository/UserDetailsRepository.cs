using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Darknet.Models;

namespace Darknet.Repository
{
    public class UserDetailsRepository : IUserDetailsRepository
    {
        private string _connectionString;
        public UserDetailsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<UserDetailsModel> GetUserDetails(string username)
        {
            UserDetailsModel userDetailsModel= new UserDetailsModel();
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("uspRegisterUser", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@username", username);
                await sqlConnection.OpenAsync();
                using (SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync()) {
                    await sqlDataReader.ReadAsync();
                    if (sqlDataReader.HasRows) {
                        userDetailsModel = new UserDetailsModel()
                        {
                            FirstName = (string)sqlDataReader["FirstName"],
                            LastName = (string)sqlDataReader["LastName"],
                            Mobile= (string)sqlDataReader["Mobile"],
                            Address= (string)sqlDataReader["Address"],
                        };
                    }
                }
            }
            return userDetailsModel;
        }
    }
}
