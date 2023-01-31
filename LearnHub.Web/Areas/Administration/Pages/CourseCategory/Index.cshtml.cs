using LearnHub.Application.Common.Pagination;
using LearnHub.Application.CourseCategory.Query;
using Learnhub.Domain.ValueObjects;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearnHub.Web.Areas.Administration.Pages.CourseCategory
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public int PageCount { get; set; }
        public List<CourseCategoryViewModel> CourseCategories { get; set; } = default!;
        public async Task OnGet(CourseCategoryQuery query)
        {
            var result =await _mediator.Send(new GetCourseCategoryAdminQuery.CourseCategoryQueryParams()
            {
                Name = query.Name,
                Current = query.Current,
            });

            CourseCategories = result.Data.Adapt<List<CourseCategoryViewModel>>();
            PageCount = result.PageCount;
        }
    }

    public class CourseCategoryQuery : Paginate<CourseCategoryViewModel>
    {
        public string Name { get; set; }
    }

    public class CourseCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CourseCount { get; set; }
        public Seo Seo { get; set; }
        public int? ParentId { get; set; }
        public List<string> SubCourseCategoriesName { get; set; }

    }
}
