using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Darknet.Models;
using Darknet.Repository;

namespace Darknet.Api.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsApiController : ControllerBase
    {
        IUserDetailsRepository _userDetailsRepository;
        public UserDetailsApiController(IUserDetailsRepository userDetailsRepository) {
            _userDetailsRepository = userDetailsRepository;
        }

        [Route("GetUserDetails")]
        [HttpGet]
        public async Task<UserDetailsModel> GetUserDetails(string username) {
            UserDetailsModel userDetailsModel;
            userDetailsModel = await _userDetailsRepository.GetUserDetails(username);
            return userDetailsModel;
        }

        [Route("StatusUpdate")]
        [HttpPost]
        public string StatusUpdate(AddPostViewModel addPostViewModel)
        {
            string result = _userDetailsRepository.AddPost(addPostViewModel.username, addPostViewModel.post, addPostViewModel.privacy);
            return result;
        }


        [Route("GetPrivacyLevels")]
        [HttpGet]
        public async Task<List<PrivacyLevelsModel>> GetPrivacyLevels()
        {
            List<PrivacyLevelsModel> lstPrivacyLevelsModel = await _userDetailsRepository.GetPrivacyLevels();
            return lstPrivacyLevelsModel;
        }

        [Route("GetAllPermissiblePosts")]
        [HttpGet]
        public async Task<List<UserPostsModel>> GetAllPermissiblePosts(string loggedInUser)
        {
            List<UserPostsModel> lstUserPostsModels = await _userDetailsRepository.GetAllPermissiblePosts(loggedInUser);
            return lstUserPostsModels;
        }

        [Route("GetPostsOfTargetUser")]
        [HttpGet]
        public async Task<List<UserPostsModel>> GetPostsOfTargetUser(string loggedInUser, string targetUser)
        {
            List<UserPostsModel> lstUserPostsModels = await _userDetailsRepository.GetPostsOfTargetUser(loggedInUser, targetUser);
            return lstUserPostsModels;
        }

    }
}