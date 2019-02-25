namespace Infrastructure.Logging
{
    public class LoggingRequest
    {
        public string Method { get; set; }

        public string Url { get; set; }

        public dynamic Body { get; set; }
    }
}
