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
        public async Task<UserDetailsModel> GetUserDetails(string username) {
            UserDetailsModel userDetailsModel = await _userDetailsRepository.GetUserDetails(username);
            return userDetailsModel;
        }
    }
}