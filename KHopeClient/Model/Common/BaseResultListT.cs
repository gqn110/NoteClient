using System.Collections.Generic;

namespace KHopeClient
{
    public class BaseResultListT<T>
    {
        public List<T> data { get; set; }
        public int? dataCount { get; set; }
        public string message { get; set; }
        public int statusCode { get; set; }
        public bool success { get; set; }
        public long timestamp { get; set; }
    }
}
