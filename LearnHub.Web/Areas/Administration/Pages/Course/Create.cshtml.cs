using LearnHub.Application.Common.Interfaces;
using LearnHub.Application.Course.Command;
using LearnHub.Application.CourseCategory.Query;
using LearnHub.Web.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LearnHub.Web.Areas.Administration.Pages.Course
{
    [IgnoreAntiforgeryToken]
    public class CreateModel : CustomPageModel
    {
        private readonly IIdentityService _identityService;
        private readonly IMediator _mediator;
        private readonly IFileUploader _fileUploader;
        public CreateModel(IIdentityService identityService, IMediator mediator, IFileUploader fileUploader)
        {
            _identityService = identityService;
            _mediator = mediator;
            _fileUploader = fileUploader;
        }
        [BindProperty]
        public CreateCourseInput Input { get; set; }
        public Dictionary<Guid,string> Teachers { get; set; }
        public SelectList Categories { get; set; }

        public async Task OnGet()
        {

            Teachers =await _identityService.GetTeachers();
            var categories = await _mediator.Send(new GetCourseCategorySelectListQuery.Request()
            {
                IsFindAllCategory = true
            });
            Categories=new SelectList(categories,"Id","Name");

        }
        public async Task<RedirectToPageResult> OnPost()
        {
            try
            {
                await _mediator.Send(new CreateCourseCommand.Request()
                {
                    Input = Input
                });

                return RedirectToPage("./Index");
            }
            catch (Exception e)
            {
                Alert(e.Message, NotificationType.Error);

                return RedirectToPage();
            }
            
        }

        public JsonResult OnPostUploadFile([FromForm] IFormFile upload)
        {
           var result= _fileUploader.Upload(upload, "CkEditorFiles");

            var success = new 
           {
               Uploaded = 1,
               FileName = result.FileName,
               Url =$"{Request.Scheme}://{Request.Host.Value}/{result.Path}"
           };
            return new JsonResult(success);
        }
    }
}
