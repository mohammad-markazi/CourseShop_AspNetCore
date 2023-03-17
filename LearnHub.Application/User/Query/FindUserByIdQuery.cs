using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnHub.Application.Common.Interfaces;
using MediatR;

namespace LearnHub.Application.User.Query
{
	public class FindUserByIdQuery
	{
		public class Request : IRequest<UserViewModel>
		{
			public Guid Id { get; set; }
		}

		public class Handler : IRequestHandler<Request, UserViewModel>
		{
			private readonly IIdentityService _identityService;

			public Handler(IIdentityService identityService)
			{
				_identityService = identityService;
			}

			public async Task<UserViewModel> Handle(Request request, CancellationToken cancellationToken)
			{
				var user = await _identityService.GetUserByIdAsync(request.Id);
				return user;
			}
		}
	}
}
