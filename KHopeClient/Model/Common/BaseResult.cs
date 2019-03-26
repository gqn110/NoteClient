using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KHopeClient
{
    public class BaseResult
    {
        public object data { get; set; }
        public int? dataCount { get; set; }
        public string message { get; set; }
        public int statusCode { get; set; }
        public bool success { get; set; }
        public long timestamp { get; set; }
    }
}
