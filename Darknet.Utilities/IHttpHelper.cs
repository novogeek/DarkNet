using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Darknet.Utilities
{
    public interface IHttpHelper
    {
        Task<V> PostAsync<T, V>(string uri, T obj);

        Task<V> GetAsync<V>(string uri);
        void AddBearerToken(string token);
    }
}
