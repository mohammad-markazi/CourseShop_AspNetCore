using LearnHub.Application.Common.Interfaces;
using LearnHub.Application.User.Command;
using LearnHub.Application.User.Query;
using LearnHub.Infrastructure.Identity;
using LearnHub.Web.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearnHub.Web.Areas.Administration.Pages.Users
{
    public class EditModel : CustomPageModel
    {
        private readonly IMediator _mediator;

        public EditModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        [BindProperty]
        public UserViewModel UserViewModel { get; set; }
        public async Task OnGet(Guid id)
        {
            UserViewModel = await _mediator.Send(new FindUserByIdQuery.Request()
            {
                Id = id
            });

        }

        public async Task<RedirectToPageResult> OnPost()
        {
            try
            {
                await _mediator.Send(new EditUserCommand.Request()
                {
                    Input = new UserInput()
                    {
                        FirstName = UserViewModel.FirstName,
                        LastName = UserViewModel.LastName,
                        Email = UserViewModel.Email,
                        Username = UserViewModel.UserName,
                        PhoneNumber = UserViewModel.PhoneNumber,
                        UserType = UserViewModel.Type,
                        Id = UserViewModel.Id
                    }
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
