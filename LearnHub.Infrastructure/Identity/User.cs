using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Learnhub.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace LearnHub.Infrastructure.Identity
{
    public class User:IdentityUser<Guid>
    {
        public string FirstName { get;private set; }
        public string LastName { get; private set; }

        public string Profile { get; private set; }

        public string Description { get; private set; }

        public UserType Type { get; private set; }

        public User()
        {
	        
        }
        public User(string username,string phoneNumber,string firstName, string lastName, UserType type,string email)
        {
	        FirstName = firstName;
	        LastName = lastName;
	        Type = type;
            UserName=username;
            PhoneNumber = phoneNumber;
            Email=email;
        }

        public void Edit(string username, string phoneNumber, string firstName, string lastName, UserType type, string email)
        {
            if(string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(UserName));
	        FirstName = firstName;
	        LastName = lastName;
	        Type = type;
	        UserName = username;
	        PhoneNumber = phoneNumber;
	        Email = email;
		}
    }

   
}
