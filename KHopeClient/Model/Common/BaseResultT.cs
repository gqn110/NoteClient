namespace KHopeClient
{
    public class BaseResultT<T>
    {
        public T data { get; set; }
        public int? dataCount { get; set; }
        public string message { get; set; }
        public int statusCode { get; set; }
        public bool success { get; set; }
        public long timestamp { get; set; }
    }
}
