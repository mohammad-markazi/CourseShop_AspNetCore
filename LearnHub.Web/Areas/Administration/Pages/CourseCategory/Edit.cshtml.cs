using LearnHub.Application.Common;
using LearnHub.Application.CourseCategory.Command.Edit;
using LearnHub.Application.CourseCategory.Query;
using Learnhub.Domain.ValueObjects;
using LearnHub.Web.Areas.Administration.Pages.CourseCategory.ViewModels;
using LearnHub.Web.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Learnhub.Domain.Exceptions;

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
        [BindProperty]
        public BaseResponse<FindCourseCategoryByIdQuery.CourseCategoryViewModel> CourseCategory { get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
            var s = Notification;
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

        public async Task<IActionResult> OnPost()
        {
            try
            {
                await _mediator.Send(new EditCourseCategoryCommand.Request()
                {
                    Id = CourseCategory.Data.Id,
                    Seo = new Seo(CourseCategory.Data.Slug, CourseCategory.Data.Keywords,
                        CourseCategory.Data.MetaDescription),
                    Name = CourseCategory.Data.Name,
                    ParentId = CourseCategory.Data.ParentId,
                });

                return RedirectToPage("./Index");
            }
            catch (NotFoundException e)
            {
                Alert(e.Message, NotificationType.Error);

               return RedirectToPage();

            }
          
            
          
           
        }



    }
}
