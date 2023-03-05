using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnHub.Application.Common.Interfaces;

namespace LearnHub.Application.User.Query
{
	public class GetUsersQueryForAdmin
	{
		public record Query() : IRequest<List<UserViewModel>>;

		public class Handler:IRequestHandler<Query,List<UserViewModel>>
		{
			private readonly IIdentityService _identityService;

			public Handler(IIdentityService identityService)
			{
				_identityService = identityService;
			}

			public async Task<List<UserViewModel>> Handle(Query request, CancellationToken cancellationToken)
			{
				var users =await _identityService.GetUsers();

				return users;
			}
		}

	}
}
