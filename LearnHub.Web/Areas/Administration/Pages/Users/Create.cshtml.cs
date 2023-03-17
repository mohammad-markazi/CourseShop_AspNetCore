using LearnHub.Application.User.Command;
using LearnHub.Infrastructure.Identity;
using LearnHub.Infrastructure.Persistence.Configuration.Identity;
using LearnHub.Web.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using static LearnHub.Web.Common.CustomPageModel;

namespace LearnHub.Web.Areas.Administration.Pages.Users
{
    public class CreateModel : CustomPageModel
    {
        private readonly IMediator _mediator;

        public CreateModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public UserInput Input { get; set; }

        public void OnGet(){}

        public async Task<RedirectToPageResult> OnPost()
        {
            try
            {
                await _mediator.Send(new CreateUserCommand.Request()
                {
                    Input = Input
                });
                return RedirectToPage("./Index");
            }
            catch (IdentityException e)
            {
	            Alert(e.ErrorMessage, NotificationType.Error);
	            return RedirectToPage();
            }
        }
    }

  
}
