using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Darknet.Utilities
{
    public interface IHttpHelper
    {
        Task<string> PostAsync<T>(string uri, T obj);
    }
}
