﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnHub.Application.Common;
using Learnhub.Domain.ValueObjects;
using LearnHub.Application.Common.Pagination;
using LearnHub.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnHub.Application.CourseCategory.Query
{
    public class GetCourseCategoryAdminQuery
    {

        public class  CourseCategoryQueryParams:Paginate<Learnhub.Domain.Entities.Course.CourseCategory>,IRequest<BaseResponse<List<CourseCategoryViewModel>>>
        {
            public string Name { get; set; }


        }


        public class  Handler:IRequestHandler<CourseCategoryQueryParams,BaseResponse<List<CourseCategoryViewModel>>>
        {
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<BaseResponse<List<CourseCategoryViewModel>>> Handle(CourseCategoryQueryParams request, CancellationToken cancellationToken)
            {
                var response = new BaseResponse<List<CourseCategoryViewModel>>();
                var courseCategories = _context.CourseCategories.Include(x=>x.Children).AsNoTracking();
                if (!string.IsNullOrEmpty(request.Name))
                    courseCategories = courseCategories.Where(x => x.Name.Contains(request.Name));

                courseCategories =request.GetPaginated(courseCategories);


                response.Data= await courseCategories.OrderByDescending(x => x.Id).Select(x => new CourseCategoryViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Seo = x.Seo,
                    CourseCount = x.CourseCount,
                    ParentId = x.ParentId,
                    SubCourseCategoriesName = x.Children.Select(x=>x.Name).ToList()
                }).ToListAsync(cancellationToken);

                response.PageCount= (response.Data.Count / request.Count) + 1;


                return response;
            }
        }

        public class CourseCategoryViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int CourseCount { get;  set; }
            public Seo Seo { get;  set; }
            public int? ParentId { get;  set; }
            public List<string> SubCourseCategoriesName { get; set; }
        }

    }
}