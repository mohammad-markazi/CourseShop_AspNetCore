using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnhub.Domain.Exceptions
{
    public class NotFoundException:Exception
    {
        public int StatusCode { get; set; }
        public NotFoundException(string message):base(message)
        {

        }

        public NotFoundException(int statusCode,string message) : base(message)
        {
            StatusCode=statusCode;

        }
    }
}
