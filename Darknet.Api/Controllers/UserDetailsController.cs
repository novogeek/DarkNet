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
    public class UserDetailsController : ControllerBase
    {
        IUserDetailsRepository _userDetailsRepository;
        public UserDetailsController(IUserDetailsRepository userDetailsRepository) {
            _userDetailsRepository = userDetailsRepository;
        }

        [Route("GetUserDetails")]
        [HttpGet]
        public async Task<UserDetailsViewModel> GetUserDetails(string loggedInUser, string targetUser) {
            UserDetailsModel userDetailsModel;
            List<PrivacyLevelsModel> lstPrivacyLevelsModel = await _userDetailsRepository.GetPrivacyLevels();
            List<UserPostsModel> lstUserPostsModels;
            if (!String.IsNullOrEmpty(targetUser))
            {
                userDetailsModel = await _userDetailsRepository.GetUserDetails(targetUser);
                lstUserPostsModels = await _userDetailsRepository.GetPostsOfTargetUser(loggedInUser, targetUser);
            }
            else {
                userDetailsModel = await _userDetailsRepository.GetUserDetails(loggedInUser);
                lstUserPostsModels = await _userDetailsRepository.GetAllPermissiblePosts(loggedInUser);
            }
            UserDetailsViewModel userDetailsViewModel = new UserDetailsViewModel
            {
                FirstName = userDetailsModel.FirstName,
                LastName = userDetailsModel.LastName,
                Address = userDetailsModel.Address,
                Mobile = userDetailsModel.Mobile,
                FriendsListDict = userDetailsModel.Friends
                    .OrderByDescending(d => d.PrivacyLevel)
                    .ThenBy(b => b.FirstName)
                    .GroupBy(f => f.PrivacyLevel)
                    .ToDictionary(g => g.Key, g => g.ToList()),
                lstPrivacyLevelsModel = lstPrivacyLevelsModel,
                lstUserPostsModels = lstUserPostsModels
            };

            return userDetailsViewModel;
        }

        [Route("StatusUpdate")]
        [HttpPost]
        public string StatusUpdate(AddPostViewModel addPostViewModel)
        {
            string result = _userDetailsRepository.AddPost(addPostViewModel.username, addPostViewModel.post, addPostViewModel.privacy);
            return result;
        }

        /*
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
        */
    }
}