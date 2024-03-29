﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnHub.Application.Common.Interfaces;
using Learnhub.Domain.Entities.Course;
using Learnhub.Domain.ValueObjects;
using LearnHub.Application.Course.Query;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace LearnHub.Application.Course.Command
{
    public class CreateCourseCommand
    {
       public class Request : IRequest<Unit>
        {
            public CreateCourseInput Input { get; set; }
        }

        class Handler : IRequestHandler<Request, Unit>
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
                var fileResult = _fileUploader.Upload(request.Input.Image, "CourseImages");

                var course = new Learnhub.Domain.Entities.Course.Course(request.Input.Title, request.Input.Description,
                    request.Input.Level, request.Input.Price, request.Input.TeacherId, request.Input.CategoryId,
                    request.Input.Published,request.Input.Seo,fileResult.Path);
               await _context.Courses.AddAsync(course,cancellationToken);
              await _context.SaveChangesAsync(cancellationToken);
              return Unit.Value;
            }
        }

    }

    public class CreateCourseInput
    {
        [Required(ErrorMessage = "این فیلد الزامی میباشد")]
        public string Title { get;  set; }
        [Required(ErrorMessage = "این فیلد الزامی میباشد")]
        public string Description { get;  set; }
        public Level Level { get;  set; }
        [Required(ErrorMessage = "این فیلد الزامی میباشد")]
        public long Price { get;  set; }
        [Required(ErrorMessage = "این فیلد الزامی میباشد")]

        public Guid TeacherId { get;  set; }
        [Required(ErrorMessage = "این فیلد الزامی میباشد")]
        public int CategoryId { get;  set; }
        public bool Published { get;  set; }
        public IFormFile Image { get; set; }
        public Seo Seo { get; set; }
    }
}
