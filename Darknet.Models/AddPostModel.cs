using System;
using System.Collections.Generic;
using System.Text;

namespace Darknet.Models
{
    public class AddPostModel
    {
        public string post { get; set; }
        public string privacy { get; set; }
    }

    public class AddPostViewModel
    {
        public string post { get; set; }
        public string privacy { get; set; }
        public string username { get; set; }
    }
}
