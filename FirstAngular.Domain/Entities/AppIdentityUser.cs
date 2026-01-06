using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FirstAngular.Domain.Entities
{
    public class AppIdentityUser: IdentityUser
    {
        public string FirstName { get; private set; } = string.Empty;
        public string MiddleInitial { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;

        protected AppIdentityUser() { }

        public static AppIdentityUser Create (string firstName, string? middleInitial, string lastName, string email)
        {
            return new AppIdentityUser
            {
                FirstName = firstName,
                MiddleInitial = middleInitial ?? string.Empty,
                LastName = lastName,
                Email = email,
                UserName = email,
                EmailConfirmed = true
            };
        }
        public bool UpdateProfile(string firstName,string? middleInitial,string lastName)
        {
            if (FirstName == firstName && MiddleInitial == (middleInitial ?? string.Empty) && LastName == lastName)
                return false;

            FirstName = firstName;
            MiddleInitial = middleInitial ?? string.Empty;
            LastName = lastName;

            return true;
        }
    }

}
