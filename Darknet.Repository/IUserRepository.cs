using Darknet.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Darknet.Repository
{
    public interface IUserRepository
    {
        string RegisterUser(UserRegistrationModel userRegistrationModel);
        string AuthenticateUser(UserCredentials userCredentials);
    }
}
