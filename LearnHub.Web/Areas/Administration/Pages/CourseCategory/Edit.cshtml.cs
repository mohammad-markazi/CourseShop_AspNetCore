using LearnHub.Application.Common;
using LearnHub.Application.CourseCategory.Query;
using LearnHub.Web.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LearnHub.Web.Areas.Administration.Pages.CourseCategory
{
    public class EditModel : CustomPageModel
    {
        private readonly IMediator _mediator;

        public EditModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public SelectList Categories { get; set; }

        public BaseResponse<FindCourseCategoryByIdQuery.CourseCategoryViewModel> CourseCategory { get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
             CourseCategory = await _mediator.Send(new FindCourseCategoryByIdQuery.QueryParams()
            {
                Id = id
            });

             if (CourseCategory.IsSuccess == false)
             {
                Alert(CourseCategory.ErrorMessage,NotificationType.Error);
                
                 return NotFound();
             }
             var result = await _mediator.Send(new GetCourseCategorySelectListQuery.GetProductsQuery());

             Categories = new SelectList(result, "Id", "Name");
            return Page();
        }
    }
}
