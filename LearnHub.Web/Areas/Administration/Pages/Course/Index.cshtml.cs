using LearnHub.Application.Common.Pagination;
using LearnHub.Application.Course.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearnHub.Web.Areas.Administration.Pages.Course
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Page<CourseViewModel> CourseViewModel { get; set; }
        public async Task OnGet(PageRequest request)
        {
            var result = await _mediator.Send(new GetAllCourseForAdminQuery.Request()
            {
                Length = request.Length,
                Index = request.Index
            });
            CourseViewModel=result;
        }
    }
}
