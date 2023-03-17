using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnHub.Application.Common;
using LearnHub.Application.Common.Interfaces;
using Learnhub.Domain.Enums;
using MediatR;

namespace LearnHub.Application.User.Command
{
    public class CreateUserCommand
    {
        public class Request : IRequest<Unit>
        {
            public UserInput Input { get; set; }
        }

        public class Handler:IRequestHandler<Request, Unit>
        {
            private readonly IIdentityService _identityService;

            public Handler(IIdentityService identityService)
            {
                _identityService = identityService;
            }

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                 await _identityService.CreateUserAsync(request.Input.FirstName,request.Input.LastName,request.Input.PhoneNumber,request.Input.Username,request.Input.UserType,request.Input.Email);
                 return Unit.Value;

            }
        }
    }

    public class UserInput
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        [Required(ErrorMessage = ValidationMessage.Required)]
        public string Username { get; set; }
        [Required(ErrorMessage = ValidationMessage.Required)]
        public string PhoneNumber { get; set; }
        public UserType UserType { get; set; }

    }
}
