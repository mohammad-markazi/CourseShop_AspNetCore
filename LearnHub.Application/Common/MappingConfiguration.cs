using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnHub.Application.Common.Interfaces;
using LearnHub.Application.CourseCategory.Query;
using Learnhub.Domain.Enums;

namespace LearnHub.Application.Common
{
    public class MappingConfiguration
	{
        public static void Config()
        {

            TypeAdapterConfig<Learnhub.Domain.Entities.Course.CourseCategory, FindCourseCategoryByIdQuery.CourseCategoryViewModel>.NewConfig()
                .Map(dest => dest.Slug, src => src.Seo.Slug)
                .Map(dest => dest.Keywords, src => src.Seo.Keywords)
                .Map(dest => dest.MetaDescription, src => src.Seo.MetaDescription);

            TypeAdapterConfig<UserType, string>.NewConfig().Map(x => x, y => y.GetDisplayName());


        }
    }

  
}
