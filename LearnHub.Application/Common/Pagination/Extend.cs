using System.Linq.Expressions;
using Azure;

namespace LearnHub.Application.Common.Pagination
{
    public static class Extend
    {
        public static Page<TModel> Page<TModel>(this IEnumerable<TModel> query)
        {
            return Page(query, Defaults.Index, Defaults.PageLength);
        }

        public static Page<TModel> Page<TModel>(this IEnumerable<TModel> query, PageRequest pageRequest)
        {
            return Page(query, pageRequest.Index, pageRequest.Length);
        }

        public static Page<TModel> Page<TModel>(this IEnumerable<TModel> query, int index)
        {
            return Page(query, index, Defaults.PageLength);
        }

        public static Page<TModel> Page<TModel>(this IEnumerable<TModel> query, int index, int pageLength)
        {
            int count = 0;
            IEnumerable<TModel> q;

            count = query.Count();
            q = query.Skip((index - 1) * pageLength)
                .Take(pageLength);

            return new Page<TModel>()
            {
                Index = index,
                Length = pageLength,
                Count = count,
                Items = q
            };
        }

        public static Page<TModel> Page<TModel, TKey>(this IQueryable<TModel> query, Expression<Func<TModel, TKey>> orderBy)
        {
            return Page(query, orderBy, Defaults.Index, Defaults.PageLength);
        }

        public static Page<TModel> Page<TModel, TKey>(this IQueryable<TModel> query, Expression<Func<TModel, TKey>> orderBy, PageRequest pageRequest)
        {
            return Page(query, orderBy, pageRequest.Index, pageRequest.Length);
        }

        public static Page<TModel> Page<TModel, TKey>(this IQueryable<TModel> query, Expression<Func<TModel, TKey>> orderBy, int index)
        {
            return Page(query, orderBy, index, Defaults.PageLength);
        }
        public static Page<TModel> Page<TModel>(this IQueryable<TModel> query, int index)
        {
            return Page(query, index, Defaults.PageLength);
        }
        public static Page<TModel> Page<TModel>(this IQueryable<TModel> query, int index, int pageLength)
        {
            int count = 0;

            count = query.Count();
            query = query
                .Skip((index - 1) * pageLength)
                .Take(pageLength);

            return new Page<TModel>()
            {
                Index = index,
                Length = pageLength,
                Count = count,
                Items = query
            };
        }
        public static Page<TModel> Page<TModel, TKey>(this IQueryable<TModel> query, Expression<Func<TModel, TKey>> orderBy, int index, int pageLength)
        {
            int count = 0;

            count = query.Count();
            query = query.OrderBy(orderBy)
                .Skip((index - 1) * pageLength)
                .Take(pageLength);

            return new Page<TModel>()
            {
                Index = index,
                Length = pageLength,
                Count = count,
                Items = query
            };
        }

        
    }
}