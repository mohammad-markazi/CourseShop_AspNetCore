using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnHub.Application.Common.Pagination
{
    public abstract class Paginate<T>
    {
        public int PageCount { get; set; }

        public int Count { get; set; } = 10;

        public int Current { get; set; }

        public IQueryable<T> GetPaginated(IQueryable<T> items)
        {
            return items.Skip(Current * Count).Take(Count);
        }

    }
}
