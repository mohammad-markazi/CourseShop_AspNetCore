using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Learnhub.Domain.Enums;
using LearnHub.Application.Common.Pagination;

namespace LearnHub.Application.Common.Interfaces
{
    public interface IIdentityService
    {
	    Task CreateUserAsync(string firstName, string lastName, string phoneNumber, string username, UserType type,string email);
	    Task EditUserAsync(Guid id,string firstName, string lastName, string phoneNumber, string username, UserType type, string email);

		Task<UserViewModel> GetUserByIdAsync(Guid id);

		Task<Page<UserViewModel>> GetUsers(PageRequest request);
        Task<Dictionary<Guid,string>> GetTeachers();


    }

    public class UserViewModel
    {
	    public Guid Id { get; set; }
	    public string UserName { get; set; }
	    public string PhoneNumber { get; set; }
	    public string FirstName { get; set; }
	    public string LastName { get; set; }
	    public string TypeName { get;  set; }
		public UserType Type { get; set; }
        public string? Email { get; set; }
    }
}
