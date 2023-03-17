using LearnHub.Application.Common.Interfaces;
using LearnHub.Application.Common.Pagination;
using LearnHub.Application.User.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearnHub.Web.Areas.Administration.Pages.Users
{
    public class IndexModel : PageModel
    {

	    private readonly IMediator _mediator;

	    public IndexModel(IMediator mediator)
	    {
		    _mediator = mediator;
	    }
		public Page<UserViewModel> Users { get; set; }
	    public async Task OnGet(PageRequest request)
	    {
		    Users =await _mediator.Send(new GetUsersQueryForAdmin.Query()
            {
				Index = request.Index
            });
	    }
    }
}
