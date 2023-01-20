using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Learnhub.Domain.Common;
using Learnhub.Domain.ValueObjects;

namespace Learnhub.Domain.Entities.Course
{
    public class CourseCategory:BaseEntity<int>
    {
        public string Name { get; private set; }
        public int CourseCount { get;private set; }
        public Seo Seo { get;private set; }

        public int? ParentId { get; private set; }
        [ForeignKey(nameof(ParentId))]
        public CourseCategory Parent { get; set; }

        public List<CourseCategory> Children { get; set; }

        public CourseCategory(string name, Seo seo, int? parentId)
        {
            Name = name;
            ParentId = parentId;
            Seo=seo;
        }


        public void SetCourseCount() => CourseCount=0;

    }
}
