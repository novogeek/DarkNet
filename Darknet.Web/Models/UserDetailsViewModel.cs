using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Darknet.Models;

namespace Darknet.Web.Models
{
    public class UserDetailsViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public Dictionary<string,List<Friend>> FriendsListDict { get; set; }
        public List<PrivacyLevelsModel> lstPrivacyLevelsModel { get; set; }
    }
}
