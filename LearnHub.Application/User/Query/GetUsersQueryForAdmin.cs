using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnHub.Application.Common.Interfaces;
using LearnHub.Application.Common.Pagination;

namespace LearnHub.Application.User.Query
{
	public class GetUsersQueryForAdmin
	{
		public class Query:PageRequest, IRequest<Page<UserViewModel>>
        {
            
        }
	
		public class Handler:IRequestHandler<Query, Page<UserViewModel>>
		{
			private readonly IIdentityService _identityService;

			public Handler(IIdentityService identityService)
			{
				_identityService = identityService;
			}

			public async Task<Page<UserViewModel>> Handle(Query request, CancellationToken cancellationToken)
			{
				var users =await _identityService.GetUsers(request);

				return users;
			}
		}

	}
}
