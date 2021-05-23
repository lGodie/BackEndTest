using System;
using System.Collections.Generic;
using System.Text;

namespace BackEndTest.Shared.Responses
{
    public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }

    }
}
