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

        public async Task<List<PrivacyLevelsModel>> GetPrivacyLevels() {

            List<PrivacyLevelsModel> lstPrivacyLevelsModel = new List<PrivacyLevelsModel>();
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("uspGetPrivacyLevels", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
    
                await sqlConnection.OpenAsync();
                using (SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync()) {
                    while (await sqlDataReader.ReadAsync()) {
                        if (sqlDataReader.HasRows)
                        {
                            lstPrivacyLevelsModel.Add(new PrivacyLevelsModel {
                                code = (string)sqlDataReader["code"],
                                value = (string)sqlDataReader["value"]
                            });
                        }
                    };
                }
            }
            return lstPrivacyLevelsModel;
        }

        public async Task<List<UserPostsModel>> GetAllPermissiblePosts(string loggedInUser)
        {
            try
            {
                List<UserPostsModel> lstUserPostsModels = new List<UserPostsModel>();
                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("uspGetAllPermissiblePosts", sqlConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@loggedInUser", loggedInUser);
                    await sqlConnection.OpenAsync();
                    using (SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        while (await sqlDataReader.ReadAsync())
                        {
                            if (sqlDataReader.HasRows)
                            {
                                lstUserPostsModels.Add(new UserPostsModel
                                {
                                    post = (string)sqlDataReader["post"],
                                    name = (string)sqlDataReader["name"],
                                    privacy = (string)sqlDataReader["privacy"],
                                    timestamp = (string)sqlDataReader["timestamp"].ToString(),
                                });
                            }
                        };
                    }
                }
                return lstUserPostsModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<UserPostsModel>> GetPostsOfTargetUser(string loggedInUser, string targetUser)
        {
            try
            {
                List<UserPostsModel> lstUserPostsModels = new List<UserPostsModel>();
                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("uspGetPostsOfTargetUser", sqlConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@loggedInUser", loggedInUser);
                    sqlCommand.Parameters.AddWithValue("@targetUser", targetUser);

                    await sqlConnection.OpenAsync();
                    using (SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        while (await sqlDataReader.ReadAsync())
                        {
                            if (sqlDataReader.HasRows)
                            {
                                lstUserPostsModels.Add(new UserPostsModel
                                {
                                    post = (string)sqlDataReader["post"],
                                    name = (string)sqlDataReader["name"],
                                    privacy = (string)sqlDataReader["privacy"],
                                    timestamp = (string)sqlDataReader["timestamp"].ToString(),
                                });
                            }
                        };
                    }
                }
                return lstUserPostsModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string AddPost(string username, string post, string privacy)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("uspAddPost", sqlConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@username", username);
                    sqlCommand.Parameters.AddWithValue("@post", post);
                    sqlCommand.Parameters.AddWithValue("@privacy", privacy);

                    sqlConnection.Open();
                    string result = sqlCommand.ExecuteScalar().ToString();
                    sqlConnection.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
