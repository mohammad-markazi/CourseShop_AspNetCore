using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Learnhub.Domain.ValueObjects;
using MediatR;

namespace LearnHub.Application.CourseCategory.Command.Create
{
    public class CreateCourseCategoryCommand
    {

        public class  Request:IRequest<Unit>
        {
            public string Name { get;  set; }
            public int CourseCount { get;  set; }
            public Seo Seo { get;  set; }
            public int? ParentId { get; set; }
        }

        public class Handler:IRequestHandler<Request,Unit>
        {
            public Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {

                return Unit.Value;
            }
        }


    }
}
