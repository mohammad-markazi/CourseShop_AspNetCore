using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using LearnHub.Application.Common.Interfaces;
using LearnHub.Infrastructure.Identity;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace LearnHub.Infrastructure.Persistence.Configuration.Identity
{
    public class IdentityService: IIdentityService
    {
	    private readonly UserManager<User> _userManager;

	    public IdentityService(UserManager<User> userManager)
	    {
		    _userManager = userManager;
	    }

	    public async Task CreateUserAsync(string firstName, string lastName, string phoneNumber, string username, int type,string description)
	    {
		 var result=  await _userManager.CreateAsync(new User(username,firstName,lastName,description,(UserType)type),username);
		 if (!result.Succeeded)
		 {
			 
			 var ErrorJson = JsonConvert.SerializeObject(result.Errors.ToList());
			 throw new ApplicationException(ErrorJson);
		 }

	    }

	    public async Task<List<UserViewModel>> GetUsers()
	    {
		    var users =await _userManager.Users.ProjectToType<UserViewModel>().ToListAsync();

		    return users;
	    }
    }
}
