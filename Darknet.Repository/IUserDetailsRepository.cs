using Darknet.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Darknet.Repository
{
    public interface IUserDetailsRepository
    {
        Task<UserDetailsModel> GetUserDetails(string username);
        Task<List<PrivacyLevelsModel>> GetPrivacyLevels();
        Task<List<UserPostsModel>> GetAllPermissiblePosts(string loggedInUser);
        Task<List<UserPostsModel>> GetPostsOfTargetUser(string loggedInUser, string targetUser);
        string AddPost(string username, string post, string privacy);
    }
}
