using System;
using System.Collections.Generic;
using System.Text;

namespace Darknet.Models
{
    public class ConfigOptions
    {
        public string WebBaseUrl { get; set; }
        public string ApiBaseUrl { get; set; }
        public string IdpLoginUrl { get; set; }
        public string IdpImpersonationUrl { get; set; }
        public string SigningKey { get; set; }
    }
}
