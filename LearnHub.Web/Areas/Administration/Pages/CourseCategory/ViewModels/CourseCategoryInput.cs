using System.ComponentModel.DataAnnotations;

namespace LearnHub.Web.Areas.Administration.Pages.CourseCategory.ViewModels
{
    public class CourseCategoryInput
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "این فیلد الزامی میباشد")]
        public string Name { get; set; }
        [Required(ErrorMessage = "این فیلد الزامی میباشد")]

        public string Slug { get; set; }
        [Required(ErrorMessage = "این فیلد الزامی میباشد")]
        public string Keywords { get; set; }
        [Required(ErrorMessage = "این فیلد الزامی میباشد")]
        public string MetaDescription { get; set; }

        public int? ParentId { get; set; }

    }
}
