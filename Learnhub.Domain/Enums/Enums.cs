using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Learnhub.Domain.Enums
{
    public enum UserType
    {
        [Display(Name = "دانشجو")]
        Student,
        [Display(Name = "استاد")]
        Teacher,
        [Display(Name = "مدیر")]
        Admin
    }



    public static class EnumExtension
    {
        public static string? GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()!
                .GetName();
        }
    }
}
