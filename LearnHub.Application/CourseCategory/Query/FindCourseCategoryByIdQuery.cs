using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnHub.Application.Common;
using Learnhub.Domain.ValueObjects;
using LearnHub.Infrastructure.Persistence;
using Mapster;
using MediatR;

namespace LearnHub.Application.CourseCategory.Query
{
    public class FindCourseCategoryByIdQuery
    {

       public class QueryParams : IRequest<BaseResponse<CourseCategoryViewModel>>
        {
            public int Id { get; set; }
        }

        class Handler : IRequestHandler<QueryParams, BaseResponse<CourseCategoryViewModel>>
        {
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<BaseResponse<CourseCategoryViewModel>> Handle(QueryParams request, CancellationToken cancellationToken)
            {
                var result = new BaseResponse<CourseCategoryViewModel>();

                var courseCategory = await _context.CourseCategories.FindAsync(request.Id);

                if (courseCategory == null)
                {
                    result.IsSuccess=false;
                    result.ErrorMessage = "دسته بندی یافت نشد";
                    return result;
                }

                result.Data = courseCategory.Adapt<CourseCategoryViewModel>();
                result.IsSuccess = true;
                return result;
            }
        }
        public class CourseCategoryViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int? ParentId { get; set; }
            public string Slug { get;  set; }
            public string Keywords { get;  set; }
            public string MetaDescription { get; set; }
        }

    }
}
