using Learnhub.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnHub.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LearnHub.Application.CourseCategory.Query
{
    public class GetCourseCategorySelectListQuery
    {
        public class Request : IRequest<List<CourseCategoryViewModel>>
        {
            public bool IsFindAllCategory { get; set; }
        }

        public class Handler : IRequestHandler<Request, List<CourseCategoryViewModel>>
        {
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }


            public async Task<List<CourseCategoryViewModel>> Handle(Request request, CancellationToken cancellationToken)
            {
                var courseCategories = _context.CourseCategories.AsNoTracking();

                if (!request.IsFindAllCategory)
                {
                     courseCategories = _context.CourseCategories.Where(x => x.ParentId == null).AsNoTracking();

                }
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
