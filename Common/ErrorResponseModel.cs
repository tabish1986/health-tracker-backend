using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ErrorResponseModel
    {
        public string ErrorCode { get; set; }
        public string Description { get; set; }
        public string ErrorCategory;
    }
}
