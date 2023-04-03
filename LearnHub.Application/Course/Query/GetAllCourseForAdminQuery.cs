using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Learnhub.Domain.ValueObjects;
using LearnHub.Application.Common.Pagination;
using MediatR;
using LearnHub.Application.Common.Interfaces;
using LearnHub.Application.CourseCategory.Query;
using Learnhub.Domain.Entities.Course;

namespace LearnHub.Application.Course.Query
{
    public class GetAllCourseForAdminQuery
    {
        public class Request : PageRequest, IRequest<Page<CourseViewModel>>
        {

        }

        public class Handler : IRequestHandler<Request, Page<CourseViewModel>>
        {
            private readonly IDapperService _dapperService;

            public Handler(IDapperService dapperService)
            {
                _dapperService = dapperService;
            }

            public async Task<Page<CourseViewModel>> Handle(Request request, CancellationToken cancellationToken)
            {
                var skip = (request.Index - 1) * request.Length;

                CourseViewModel func(CourseViewModel course, Seo seo,string categoryName,string teacherName) 
                { 
                    course.Seo = new Seo(seo.Slug, seo.Keywords, seo.MetaDescription);
                    course.CategoryName= categoryName;
                    course.TeacherName= teacherName;
                    return course;
                }

                var result = await _dapperService
                    .GetAllAsync<CourseViewModel, Seo, string, string>
                        ("SP_GetCoursesForAdmin", func, CommandType.StoredProcedure, "Id,Keywords,Slug,MetaDescription,Name,TeacherName",
                            new {skip=skip,take=request.Length});
                var page = new Page<CourseViewModel>
                {
                    Items = result,
                    Index = request.Index,
                    Length = request.Length
                };

                return page;
            }


        }
    }

    public class CourseViewModel
    {

        public long Id { get; set; }
        public string Title { get; set; }
        public int CountParticipant { get; set; }
        public Level Level { get; set; }
        public long Price { get; set; }
        public string TeacherName { get; set; }
        public string CategoryName { get; set; }
        public bool Published { get; set; }
        public long CategoryId { get; set; }
        public Seo Seo { get; set; }
    }
}
