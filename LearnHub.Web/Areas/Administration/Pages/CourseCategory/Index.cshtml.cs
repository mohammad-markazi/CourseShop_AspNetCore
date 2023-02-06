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

        public Page<GetCourseCategoryAdminQuery.CourseCategoryViewModel> CourseCategories { get; set; } = default!;
        public async Task OnGet(CourseCategoryQuery query)
        {
            var result =await _mediator.Send(new GetCourseCategoryAdminQuery.CourseCategoryQueryParams()
            {
                Name = query.Name,
                Index = query.Index,
            });

            CourseCategories = result;



        }
    }

    public class CourseCategoryQuery : PageRequest
    {
        public string Name { get; set; }
    }

   
}
