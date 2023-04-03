using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Learnhub.Domain.Common;
using Learnhub.Domain.ValueObjects;

namespace Learnhub.Domain.Entities.Course
{
	public class Course:BaseEntity<long>
	{
		public string Title { get; private set; }
		public string Description { get; private set; }
		public int CountParticipant { get;private set; }
		public Level Level { get; private set; }
		public long Price { get;private set; }
		public Guid TeacherId { get; private set; }
		public int CategoryId { get; private set; }
		public bool Published { get; private set; }
		[ForeignKey(nameof(CategoryId))]
		public CourseCategory Category { get; private set; }

        public Seo Seo { get; set; }
		public List<CourseEpisode> CourseEpisodes { get; set; }
		public Course(){}

		public Course(string title, string description, Level level, long price, Guid teacherId, int categoryId, bool published,Seo seo)
		{
			if(string.IsNullOrEmpty(title))
				throw new ArgumentNullException(nameof(title));

			if(price < 0) throw new ArgumentOutOfRangeException(nameof(price));
			Title = title;
			Description = description;
			Level = level;
			Price = price;
			TeacherId = teacherId;
			CategoryId = categoryId;
			Published = published;
            Seo = seo;
        }

		public void AddEpisode(CourseEpisode episode)
		{
			this.CourseEpisodes??= new List<CourseEpisode>();
			this.CourseEpisodes.Add(episode);
		}
	}

	public enum Level
	{
		[Display(Name = "مقدماتی")]
		Beginner,
		[Display(Name = "متوسط")]
		Middle,
		[Display(Name = "پیشرفته")]
		Advanced
	}

    public class SeoViewModel:Object
    {
        public string Seo_Slug { get;  set; }
        public string Seo_Keywords { get;  set; }
        public string Seo_MetaDescription { get;  set; }

    }
}
