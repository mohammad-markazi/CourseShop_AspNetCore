using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnHub.Application.Common.Interfaces;
using MediatR;

namespace LearnHub.Application.User.Command
{
	public class EditUserCommand
	{
		public class Request:IRequest<Unit>
		{
			public UserInput Input { get; set; }

		}

		public class Handler:IRequestHandler<Request,Unit>
		{
			private readonly IIdentityService _identityService;

			public Handler(IIdentityService identityService)
			{
				_identityService = identityService;
			}

			public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
			{
				await _identityService.EditUserAsync(request.Input.Id, request.Input.FirstName, request.Input.LastName,
					request.Input.PhoneNumber, request.Input.Username, request.Input.UserType, request.Input.Email);
				return Unit.Value;
			}
		}
	}
}
