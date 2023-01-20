using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnhub.Domain.Common
{
    public class BaseEntity<TKey>
    {
        public BaseEntity()
        {
            CreateDateTime = DateTime.Now;
        }
        public TKey Id{ get; set; }


        public bool IsActive { get; set; }

        public DateTime CreateDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }

    }
}
