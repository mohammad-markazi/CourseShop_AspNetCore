using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Learnhub.Domain.Common;

namespace Learnhub.Domain.Entities.Course
{
	public class CourseEpisode:BaseEntity<long>
	{
		public string Title { get; private set; }
		public TimeSpan Time { get; private set; }
		public string FilePath { get; private set; }
		public long CourseId { get; private set; }
		public Course Course { get; set; }


	}
}
