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
    }
}
