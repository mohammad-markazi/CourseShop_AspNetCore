using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using LearnHub.Application.Common.Interfaces;
using LearnHub.Application.Common.Pagination;
using Learnhub.Domain.Enums;
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

	    public async Task CreateUserAsync(string firstName, string lastName, string phoneNumber, string username, UserType type,string email)
	    {
		 var result=  await _userManager.CreateAsync(new User(username,phoneNumber,firstName,lastName,type,email));
		 if (!result.Succeeded)
		 {
			 
			 var ErrorJson = JsonConvert.SerializeObject(result.Errors.ToList());
			 throw new IdentityException(ErrorJson);
		 }

	    }

	    public async Task EditUserAsync(Guid id,string firstName, string lastName, string phoneNumber, string username, UserType type, string email)
	    {
		    var user =await _userManager.FindByIdAsync(id.ToString());
		    if (user is null)
			    throw new IdentityException("کاربر یافت نشد");

			user.Edit(username, phoneNumber, firstName, lastName, type,email);
		await	_userManager.UpdateAsync(user);
	    }

	    public async  Task<UserViewModel> GetUserByIdAsync(Guid id)
	    {
			var user = await _userManager.FindByIdAsync(id.ToString());
			if (user is null)
				throw new IdentityException("کاربر یافت نشد");

			var userViewModel=user.Adapt<UserViewModel>();
			return userViewModel;
	    }


	    public async Task<Page<UserViewModel>> GetUsers(PageRequest request)
	    {
		    var users = _userManager.Users.Select(x=> new UserViewModel()
            {
				UserName = x.UserName,
				FirstName = x.FirstName,
				LastName = x.LastName,
				Type = x.Type,
				Email = x.Email,
				TypeName = x.Type.GetDisplayName(),
				PhoneNumber = x.PhoneNumber,
				Id = x.Id
            }).Page(request.Index,20);
            return users;
	    }

        public async  Task<Dictionary<Guid, string>> GetTeachers()
        { 
            return await _userManager.Users.Where(x => x.Type == UserType.Teacher).Select(x => new { x.Id,Name=($"{x.FirstName} {x.LastName}") }).ToDictionaryAsync(x=>x.Id,x=>x.Name);
            
        }
    }
}
