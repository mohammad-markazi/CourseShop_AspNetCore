using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnHub.Application.Common.Interfaces;
using LearnHub.Application.Course.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnHub.Application.Course.Command
{
    public class EditCourseCommand
    {
        public class Request:IRequest<Unit>
        {
            public FindCourseByIdQuery.CourseViewModel Input { get; set; }
        }


        public class Handler:IRequestHandler<Request, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IFileUploader _fileUploader;
            public Handler(IApplicationDbContext context, IFileUploader fileUploader)
            {
                _context = context;
                _fileUploader = fileUploader;
            }

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                if (request.Input.ImageUpload is not null)
                {
                    var fileResult = _fileUploader.Upload(request.Input.ImageUpload, "CourseImages");
                    _fileUploader.RemoveFile(request.Input.Image);
                    request.Input.Image = fileResult.Path;
                }
                 

                var course =await _context.Courses.SingleAsync(x => x.Id == request.Input.Id,cancellationToken);


                course.Edit(request.Input.Title,request.Input.Description,request.Input.Level,request.Input.Price,request.Input.TeacherId,request.Input.CategoryId,request.Input.Published,
                    request.Input.Seo, request.Input.Image);

               await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
        
    }
}
