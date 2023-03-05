using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnHub.Application.Common.Interfaces
{
    public interface IIdentityService
    {
	    Task CreateUserAsync(string firstName, string lastName, string phoneNumber, string username, int type,
		    string description);


	    Task<List<UserViewModel>> GetUsers();


    }

    public class UserViewModel
    {
	    public Guid Id { get; set; }
	    public string Username { get; set; }
	    public string PhoneNumber { get; set; }
	    public string FirstName { get; set; }
	    public string LastName { get; set; }
	    public int Type { get;  set; }


	}
}
