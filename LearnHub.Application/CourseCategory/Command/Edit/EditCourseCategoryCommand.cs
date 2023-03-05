using Learnhub.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Learnhub.Domain.Exceptions;

namespace LearnHub.Application.CourseCategory.Command.Edit
{
    public class EditCourseCategoryCommand
    {
        public class Request : IRequest<Unit>
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public Seo Seo { get; set; }
            public int? ParentId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                var courseCategory =await _context.CourseCategories.FindAsync(request.Id, cancellationToken);

                if (courseCategory is null)
                    throw new NotFoundException(404, "دسته بندی مورد نظر یافت نشد");

                courseCategory.Edit(request.Name,request.Seo,request.ParentId);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
