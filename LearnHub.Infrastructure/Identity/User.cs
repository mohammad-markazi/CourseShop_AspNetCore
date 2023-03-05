using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public User(string username,string firstName, string lastName, string description, UserType type)
        {
	        FirstName = firstName;
	        LastName = lastName;
	        Description = description;
	        Type = type;
            UserName=username;
        }
    }

    public enum UserType
    {
        Student,
        Teacher,
        Admin
    }
}
