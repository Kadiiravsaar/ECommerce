using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Response
{
    public class ApiDataResponse<T> : ApiResponse
    {
        public T Data { get; set; }

        public ApiDataResponse()
        {
                
        }
        public ApiDataResponse(bool success) : base(success)
        {
                
        }
        public ApiDataResponse(bool success, string message):base(success,message)
        {
                
        }
    }
}
