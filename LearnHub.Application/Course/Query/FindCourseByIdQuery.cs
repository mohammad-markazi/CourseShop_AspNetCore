using Learnhub.Domain.Entities.Course;
using Learnhub.Domain.ValueObjects;
using LearnHub.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace LearnHub.Application.Course.Query
{
    public class FindCourseByIdQuery
    {
        public class Request : IRequest<CourseViewModel>
        {
            public long Id { get; set; }
        }
        public class Handler : IRequestHandler<Request, CourseViewModel>
        {
            private readonly IDapperService _dapperService;

            public Handler(IDapperService dapperService)
            {
                _dapperService = dapperService;
            }

            public async Task<CourseViewModel> Handle(Request request, CancellationToken cancellationToken)
            {
                string query = $"""
                    select Id,Title,Description,Level,Price,TeacherId,CategoryId,Published,Image,Seo_Slug as Slug,Seo_Keywords as Keywords,Seo_MetaDescription as MetaDescription from Courses where id={request.Id}
                    """;
                CourseViewModel func(CourseViewModel course, Seo seo)
                {
                    course.Seo = new Seo(seo.Slug, seo.Keywords, seo.MetaDescription);
                    return course;
                }

                var result = await _dapperService.GetAllAsync<CourseViewModel, Seo>(query, func, CommandType.Text,
                    "Id,Keywords,MetaDescription,Slug");
                var course = result.First();
                return course;
            }
        }
        public class CourseViewModel
        {
            public long Id { get; set; }
            [Required(ErrorMessage = "این فیلد الزامی میباشد")]
            public string Title { get; set; }
            [Required(ErrorMessage = "این فیلد الزامی میباشد")]
            public string Description { get; set; }
            public Level Level { get; set; }
            [Required(ErrorMessage = "این فیلد الزامی میباشد")]
            public long Price { get; set; }
            [Required(ErrorMessage = "این فیلد الزامی میباشد")]
            public Guid TeacherId { get; set; }
            [Required(ErrorMessage = "این فیلد الزامی میباشد")]
            public int CategoryId { get; set; }
            public bool Published { get; set; }
            public string Image { get; set; }
            public IFormFile? ImageUpload { get; set; }
            public Seo Seo { get; set; }
        }
    }


}
