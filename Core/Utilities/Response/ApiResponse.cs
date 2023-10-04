using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Response
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ApiResponse()
        {
        }

        public ApiResponse(bool success)
        {
              Success = success;
        }
        public ApiResponse(bool success,string message)
        {
            Success = success;
            Message = message;
        }
    }
}
