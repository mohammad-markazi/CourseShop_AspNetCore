using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnHub.Application.Common
{
    public class BaseResponse<T>
    {
        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }

        public T Data { get; set; }
    }
}
