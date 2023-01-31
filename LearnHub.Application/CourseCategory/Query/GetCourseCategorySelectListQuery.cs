using Learnhub.Domain.ValueObjects;
using LearnHub.Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static LearnHub.Application.CourseCategory.Query.GetCourseCategoryAdminQuery;

namespace LearnHub.Application.CourseCategory.Query
{
    public class GetCourseCategorySelectListQuery
    {
        public record GetProductsQuery() : IRequest<List<CourseCategoryViewModel>>;

        public class Handler : IRequestHandler<GetProductsQuery, List<CourseCategoryViewModel>>
        {
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }


            public async Task<List<CourseCategoryViewModel>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
            {
                var courseCategories = _context.CourseCategories.Where(x=>x.ParentId==null).AsNoTracking();
     

                return await courseCategories.OrderByDescending(x => x.Id).Select(x => new CourseCategoryViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync(cancellationToken);
            }
        }

        public class CourseCategoryViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
