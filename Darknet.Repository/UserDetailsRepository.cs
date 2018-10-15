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
                SqlCommand sqlCommand = new SqlCommand("uspGetUserDetails", sqlConnection)
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
                            Mobile = (string)sqlDataReader["Mobile"],
                            Address = (string)sqlDataReader["Address"],
                            Friends = new List<Friend>()
                        };
                        await sqlDataReader.NextResultAsync();
                        while (await sqlDataReader.ReadAsync()) {
                            userDetailsModel.Friends.Add(new Friend() {
                                FirstName = (string)sqlDataReader["FirstName"],
                                LastName = (string)sqlDataReader["LastName"],
                                Username= (string)sqlDataReader["Username"],
                                PrivacyLevel = (string)sqlDataReader["PrivacyLevel"],
                            });
                        }
                    }
                }
            }
            return userDetailsModel;
        }
    }
}
