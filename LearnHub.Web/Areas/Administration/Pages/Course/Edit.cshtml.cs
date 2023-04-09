using LearnHub.Application.Common.Interfaces;
using LearnHub.Application.Course.Command;
using LearnHub.Application.Course.Query;
using LearnHub.Application.CourseCategory.Query;
using LearnHub.Web.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using static LearnHub.Web.Common.CustomPageModel;

namespace LearnHub.Web.Areas.Administration.Pages.Course
{
    public class EditModel : CustomPageModel
    {
        private readonly IMediator _mediator;
        private readonly IIdentityService _identityService;
        public EditModel(IMediator mediator, IIdentityService identityService)
        {
            _mediator = mediator;
            _identityService = identityService;
        }

        [BindProperty]
        public FindCourseByIdQuery.CourseViewModel Input { get; set; }
        public Dictionary<Guid, string> Teachers { get; set; }  
        public SelectList Categories { get; set; }
        public async Task OnGet(long id)
        {

            Teachers = await _identityService.GetTeachers();
            var categories = await _mediator.Send(new GetCourseCategorySelectListQuery.Request()
            {
                IsFindAllCategory = true
            });
            Categories = new SelectList(categories, "Id", "Name"); 
            
            Input =await _mediator.Send(new FindCourseByIdQuery.Request()
            {
                Id = id
            });
           

        }

        public async Task<ActionResult> OnPost()
        {
            try
            {
                await _mediator.Send(new EditCourseCommand.Request()
                {
                    Input = Input
                });
                return RedirectToPage("./Index");
            }
            catch (Exception e)
            {

                Alert(e.Message, NotificationType.Error);
                return Page();
            }

        }
    }
}
