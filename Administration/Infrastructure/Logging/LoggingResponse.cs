using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Logging
{
    public class LoggingResponse
    {
        public int StatusCode { get; set; }

        public dynamic Body { get; set; }
    }
}
