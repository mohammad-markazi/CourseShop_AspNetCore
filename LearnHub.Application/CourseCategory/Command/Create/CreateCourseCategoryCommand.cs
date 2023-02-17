using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Learnhub.Domain.ValueObjects;
using LearnHub.Infrastructure.Persistence;
using MediatR;
using Learnhub.Domain.Entities.Course;
namespace LearnHub.Application.CourseCategory.Command.Create
{
    public class CreateCourseCategoryCommand
    {

        public class  Request:IRequest<Unit>    
        {
            public string Name { get;  set; }
            public Seo Seo { get;  set; }
            public int? ParentId { get; set; }
        }

        public class Handler:IRequestHandler<Request,Unit>
        {
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                var courseCategory = new Learnhub.Domain.Entities.Course.CourseCategory(request.Name,request.Seo,request.ParentId);

               await _context.CourseCategories.AddAsync(courseCategory,cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }


    }
}
